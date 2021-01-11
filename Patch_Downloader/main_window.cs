using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using System.Threading;
//using Microsoft.WindowsAPICodePack.Dialogs;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.ExceptionServices;

using System.Windows.Forms.DataVisualization.Charting;
namespace Huinno_Downloader
{
    public partial class main_window : Form
    {
        bool m_dev = false;
        public bool m_loginPass;
        string m_loginId;
        string m_loginPw;
        public bool m_progRunning = false;

        //
        //const double m_timer_logout_period = 5 * 1000;
        const double m_timer_logout_period = 30 * 60 * 1000;
        // string
        string m_str_failed_to_connect = "Connection Failed! Chem_config_comportck COM port or reconnect patch to PC.";
        string m_str_download_completed = "Open web page to upload data.";
        string m_str_download_completed_title = "Download completed";
        string m_str_not_setup_url = "Please set url.";

        // params for config
        string m_strCfg_comport;
        string m_strCfg_baudrate;
        string m_strCfg_savepath;
        public string m_strCfg_uploadurl;   // public for check in main()

        //
        string m_curSavePath;

        string m_resFileName;       // only file_name without exp
        string m_resFilePath;       // dir + file_name
        string m_resFilePathExp;    // dir + file_name + exp

        //
        Thread m_RdSerialDataThd;
        Thread m_CheckRunningThd;

        DOWN_MODE_T m_downMode = 0;

        // for header
        byte[] m_EcgHeader = new byte[32];
        int m_pairStTime = -1;
        int m_pairStPos = 0;

        // uart 
        const int UART_RX_CHAR_LEN_MAX = 16;
        byte[] m_uartSendMsg = new byte[UART_RX_CHAR_LEN_MAX];

        FileStream m_fs;
        BinaryWriter m_bw;
        int m_wrCnt = 0;

        public enum UART_RX_CMD_T
        {
            URX_CMD_SET_PRODUCT_NAME = 0,   // 0
            URX_CMD_INIT_NAND_PAGE,         // 1
            URX_CMD_RD_NAND_ECG_DATA,       // 2
            URX_CMD_RD_NAND_USER_MARK,      // 3
            URX_CMD_SYSTEM_RESET,           // 4
            URX_CMD_GET_SYS_INFO,           // 5
            URX_CMD_RD_NAND_PAGE_ST_ED,     // 6
            UART_RX_CMD_NUM_MAX
        }

        public enum PD_NAME_T
        {
            PD_NAME_HUINNO = 0,
            PD_NAME_CATEGORY,
            PD_NAME_ASSMTYPE,
            PD_NAME_VERSION,
            PD_NAME_USAGETYPE,
            PD_NAME_COUNTRY,
            PD_NAME_SERIALNUM,
            PD_NAME_NUM_MAX
        }

        public enum DOWN_MODE_T
        {
            DOWN_MODE_USRMARK = 0,        // 0
            DOWN_MODE_ECGDATA,         // 1
            DOWN_MODE_NUM_MAX
        }

        public enum USER_MARK_T
        {
            USER_MARK_PAIRING_ST = 0,        // 0
            USER_MARK_PAIRING_ED,         // 1
            USER_MARK_EVENT_APP,        // 2
            USER_MARK_EVENT_BTN,        // 3
            USER_MARK_TYPE_NUM_MAX
        }

        // product name
        bool m_isProductNameSet = false;

        string[] m_productName = new string[(int)PD_NAME_T.PD_NAME_NUM_MAX];
        string m_curSerialName;
        byte[] m_productNameByte = new byte[13];

        // nand index
        string[] m_strNandIdx_St = new string[(int)DOWN_MODE_T.DOWN_MODE_NUM_MAX];
        string[] m_strNandIdx_Ed = new string[(int)DOWN_MODE_T.DOWN_MODE_NUM_MAX];
        int[] m_nandIdx_St = new int[(int)DOWN_MODE_T.DOWN_MODE_NUM_MAX];
        int[] m_nandIdx_Ed = new int[(int)DOWN_MODE_T.DOWN_MODE_NUM_MAX];
        int m_RdPageTotalCnt;


        // flag to control thread
        bool m_startThd_2nd = false;
        bool m_stopThd_1st = false;
        bool m_stopThd_2nd = false;

        /// <summary>
        /// Select memory size
        /// </summary>
        //////////////////////////////////////////////////
        const int USER_MARK_SIZE = 32;
        const int MEM_INIT_VAL = 0xFF;

        const int MEM_4G_PAGE_FULL_SZ = 4096;
        const int MEM_4G_PAGE_USE_SZ = 4095;
        const int MEM_2G_PAGE_FULL_SZ = 2048;
        const int MEM_2G_PAGE_USE_SZ = 2046;

        const int MEM_PAGE_SZ = MEM_4G_PAGE_FULL_SZ;
        const int MEM_PAGE_USE_SZ = MEM_4G_PAGE_USE_SZ;
       // const int MEM_PAGE_SZ = MEM_2G_PAGE_FULL_SZ;
       // const int MEM_PAGE_USE_SZ = MEM_2G_PAGE_USE_SZ;
        //////////////////////////////////////////////////
        
        //
        System.Timers.Timer timer_logout = new System.Timers.Timer();

        login_window m_form_login = new login_window();

        public main_window()
        {
           
            m_form_login.ProgRunning = m_progRunning;
            //m_form_login.ShowDialog();

            m_form_login.LoginPass = true;
            m_loginPass = m_form_login.LoginPass;
            if (!m_loginPass)
                return;

            m_loginId = m_form_login.LoginId;
            m_loginPw = m_form_login.LoginPw;
         

            //
            InitializeComponent();
            CB_ComPortBaudList.SelectedIndex = 0;
            m_progRunning = true;

            initUserParams();
            initUserUi();
            timer_logout.Interval = m_timer_logout_period;
            timer_logout.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);


            timer_logout.Start();
            string strVersionText = Assembly.GetExecutingAssembly().FullName
           .Split(',')[1]
           .Trim()
           .Split('=')[1];

            this.Text = this.Text +" ver " +  strVersionText.Substring(2,5);

        }

        delegate void TimerEventFiredDelegate();
        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            BeginInvoke(new TimerEventFiredDelegate(tmrLogoutProc));
        }

        private void tmrLogoutProc()
        {
            timer_logout.Stop();

            //
            m_form_login.LoginPass = false;
            m_form_login.ProgRunning = m_progRunning;
            this.Hide();
            m_form_login.ShowDialog();

            //
            m_loginPass = m_form_login.LoginPass;
            if (!m_loginPass)
            {
                Application.ExitThread();
                Environment.Exit(0);
                return;
            }

            //
            this.Show();
            timer_logout.Start();
        }

        void initUserParams()
        {
            // config: comport
            m_strCfg_comport = AppConfiguration.GetAppConfig("ComPortName");
            m_strCfg_baudrate = CB_ComPortBaudList.SelectedItem.ToString();
            //m_config_baudrate = AppConfiguration.GetAppConfig("ComPortBaud");
            m_strCfg_savepath = AppConfiguration.GetAppConfig("SavePath");
            m_strCfg_uploadurl = AppConfiguration.GetAppConfig("UploadUrl");

            if (m_strCfg_uploadurl == "")
            {
                MessageBox.Show("Please set url.");
            }
            // set save path
            string strSavePath = (m_strCfg_savepath != "") ? m_strCfg_savepath : Application.StartupPath + "\\Downloads";
            setSavePath(strSavePath);

            // get comport name list
            RefreshComPortList();
        }

        void initUserUi()
        {
            BT_LogOut.Visible = false;

            // Init val
            ControlLabel(LB_ProgVal, "0");

            TB_Serial_Init();

        }

        void setSavePath( string newSavePath)
        {
            if (m_curSavePath == newSavePath)
                return;

            m_curSavePath = newSavePath;
            CreateSaveDir(newSavePath);

            TB_SavePath.Text = newSavePath;
            AppConfiguration.SetAppConfig("SavePath", newSavePath);
        }

        private void CreateSaveDir(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            if (di.Exists == false)
            {
                di.Create();
            }
        }

        private void RefreshComPortList()
        {
            string[] nameArray = cSerialPort.GetSerialComPortNameList();

            CB_ComPortNameList.Items.Clear();
            CB_ComPortNameList.Items.AddRange(nameArray);
            for (int i = 0; i < nameArray.Length; ++i)
            {
                if (nameArray[i] == m_strCfg_comport)
                {
                    CB_ComPortNameList.Text = m_strCfg_comport;
                    break;
                }
            }
            CB_ComPortBaudList.Text = m_strCfg_baudrate;
        }

        private void TB_Serial_Init()
        {
            TB_Serial1.Text = "";
            TB_Serial2.Text = "";
            TB_Serial3.Text = "";
            TB_Serial4.Text = "";
            TB_Serial5.Text = "";
            TB_Serial6.Text = "";
            TB_Serial7.Text = "";
        }

        private void TB_Serial_SetProductName()
        {
            TB_Serial1.Text = m_productName[(int)PD_NAME_T.PD_NAME_HUINNO]    ;
            TB_Serial2.Text = m_productName[(int)PD_NAME_T.PD_NAME_CATEGORY ] ;
            TB_Serial3.Text = m_productName[(int)PD_NAME_T.PD_NAME_ASSMTYPE ] ;
            TB_Serial4.Text = m_productName[(int)PD_NAME_T.PD_NAME_VERSION  ] ;
            TB_Serial5.Text = m_productName[(int)PD_NAME_T.PD_NAME_USAGETYPE] ;
            TB_Serial6.Text = m_productName[(int)PD_NAME_T.PD_NAME_COUNTRY  ] ;
            TB_Serial7.Text = m_productName[(int)PD_NAME_T.PD_NAME_SERIALNUM]; 
        }

        private void BT_OpenSavePath_Click(object sender, EventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(TB_SavePath.Text);
            if (di.Exists)
            {
                Process.Start(TB_SavePath.Text);
            }
        } 

        private void BT_ConnPort_Click(object sender, EventArgs e)
        {
            //
            timer_logout.Stop();
            timer_logout.Start();

            if (!cSerialPort.isConnected)
            {
                CB_ComPortBaudList.SelectedIndex = 0;
                int selPortIdx = CB_ComPortNameList.SelectedIndex;
                if (selPortIdx < 0)
                    return;

                int selBaudIdx = CB_ComPortBaudList.SelectedIndex;
                if (selBaudIdx < 0)
                    return;
                 
                int baudrate = Int32.Parse(CB_ComPortBaudList.Text);
                string err = cSerialPort.Open(CB_ComPortNameList.Text, baudrate);
                if( err!="OK")
                {
                    CloseSerial();
                    return;
                }
                AppConfiguration.SetAppConfig("ComPortName", CB_ComPortNameList.Text);
                AppConfiguration.SetAppConfig("ComPortBaud", CB_ComPortBaudList.Text);

                ControlButtonText(BT_ConnPort, "Disconnect");
            }
            else
            {
                CloseSerial();
            }
        }

        private void CloseSerial()
        {
            cSerialPort.Close();
            ControlButtonText(BT_ConnPort, "Connect");
        }

        private void ParseDeviceInfo(string str_tx)
        {
            int pos = str_tx.IndexOf("[INFO] ");
            pos += 7;

            m_productName[(int)PD_NAME_T.PD_NAME_HUINNO]    = str_tx.Substring(pos, 1); pos += 2;
            m_productName[(int)PD_NAME_T.PD_NAME_CATEGORY ] = str_tx.Substring(pos, 2); pos += 3;
            m_productName[(int)PD_NAME_T.PD_NAME_ASSMTYPE ] = str_tx.Substring(pos, 1); pos += 2;
            m_productName[(int)PD_NAME_T.PD_NAME_VERSION  ] = str_tx.Substring(pos, 1); pos += 2;
            m_productName[(int)PD_NAME_T.PD_NAME_USAGETYPE] = str_tx.Substring(pos, 1); pos += 2;
            m_productName[(int)PD_NAME_T.PD_NAME_COUNTRY  ] = str_tx.Substring(pos, 2); pos += 3;
            m_productName[(int)PD_NAME_T.PD_NAME_SERIALNUM] = str_tx.Substring(pos, 5); pos += 6;

            m_productNameByte = Encoding.ASCII.GetBytes(m_productName[(int)PD_NAME_T.PD_NAME_HUINNO]
                                                        + m_productName[(int)PD_NAME_T.PD_NAME_CATEGORY]
                                                        + m_productName[(int)PD_NAME_T.PD_NAME_ASSMTYPE]
                                                        + m_productName[(int)PD_NAME_T.PD_NAME_VERSION]
                                                        + m_productName[(int)PD_NAME_T.PD_NAME_USAGETYPE]
                                                        + m_productName[(int)PD_NAME_T.PD_NAME_COUNTRY]
                                                        + m_productName[(int)PD_NAME_T.PD_NAME_SERIALNUM]);

            //
            string sub;
            sub = str_tx.Substring(pos, str_tx.Length - pos);

            pos = sub.IndexOf(".");
            m_strNandIdx_St[(int)DOWN_MODE_T.DOWN_MODE_USRMARK] = sub.Substring(0, pos);

            sub = sub.Substring(pos + 1, sub.Length - pos - 1);
            pos = sub.IndexOf(".");
            m_strNandIdx_Ed[(int)DOWN_MODE_T.DOWN_MODE_USRMARK] = sub.Substring(0, pos);

            sub = sub.Substring(pos + 1, sub.Length - pos - 1);
            pos = sub.IndexOf(".");
            m_strNandIdx_St[(int)DOWN_MODE_T.DOWN_MODE_ECGDATA] = sub.Substring(0, pos);

            sub = sub.Substring(pos + 1, sub.Length - pos - 1);
            string checkStr = sub;
            while (checkStr.Length>0)
            {
                string lastStr = checkStr.Substring(checkStr.Length - 1);
                byte [] byteVal = Encoding.UTF8.GetBytes(lastStr);
                if (byteVal[0]>=48 && byteVal[0]<=57)
                    break;

                checkStr = checkStr.Substring(0, checkStr.Length - 1);
            }
            m_strNandIdx_Ed[(int)DOWN_MODE_T.DOWN_MODE_ECGDATA] = checkStr;

            m_nandIdx_St[(int)DOWN_MODE_T.DOWN_MODE_USRMARK] = Int32.Parse(m_strNandIdx_St[(int)DOWN_MODE_T.DOWN_MODE_USRMARK]);
            m_nandIdx_Ed[(int)DOWN_MODE_T.DOWN_MODE_USRMARK] = Int32.Parse(m_strNandIdx_Ed[(int)DOWN_MODE_T.DOWN_MODE_USRMARK]);
            m_nandIdx_St[(int)DOWN_MODE_T.DOWN_MODE_ECGDATA] = Int32.Parse(m_strNandIdx_St[(int)DOWN_MODE_T.DOWN_MODE_ECGDATA]);
            m_nandIdx_Ed[(int)DOWN_MODE_T.DOWN_MODE_ECGDATA] = Int32.Parse(m_strNandIdx_Ed[(int)DOWN_MODE_T.DOWN_MODE_ECGDATA]);
        }

        private void setButtonEnabledUI(bool bEnable)
        {
            ControlButton(BT_StartDown, bEnable);
            ControlButton(BT_ConnPort, bEnable);
            ControlButton(BT_SelSaveDir, bEnable);
            ControlTextBox_Enable(TB_SavePath, bEnable);
        }

        private void BT_StartDown_Click(object sender, EventArgs e)
        {
            if (!cSerialPort.isConnected)
                return;

            // set ui
            setButtonEnabledUI(false);
            ControlProgressBar(progressBar1, 0);
            ControlLabel(LB_ProgVal, "0");

            //
            int iRetGetDeviceInfo = getDeviceInfo();
            if (iRetGetDeviceInfo < 0)
            {
                ControlTextBox(TB_LogMsg, m_str_failed_to_connect);
                setButtonEnabledUI(true);
                CloseSerial();
                return;
            }
            else if (iRetGetDeviceInfo == 0)
            {
                ControlTextBox(TB_LogMsg, "Patch info.: HEMP_" 
                    + m_curSerialName 
                    + " [" + m_strNandIdx_St[(int)DOWN_MODE_T.DOWN_MODE_USRMARK]                 
                    + "." + m_strNandIdx_Ed[(int)DOWN_MODE_T.DOWN_MODE_USRMARK]    
                    + "." + m_strNandIdx_St[(int)DOWN_MODE_T.DOWN_MODE_ECGDATA]   
                    + "." + m_strNandIdx_Ed[(int)DOWN_MODE_T.DOWN_MODE_ECGDATA] 
                    + "]");
            }
            setButtonEnabledUI(false);

            timer_logout.Stop();

            // Re connect with maximum baudrate 3M
            CloseSerial();
            Thread.Sleep(2000);
            
            CB_ComPortBaudList.SelectedIndex = 1;
            int baudrate = Int32.Parse(CB_ComPortBaudList.Text);
            string err = cSerialPort.Open(CB_ComPortNameList.Text, baudrate);
            if (err != "OK")
            {
                CloseSerial();
                return;
            }
            AppConfiguration.SetAppConfig("ComPortName", CB_ComPortNameList.Text);
            AppConfiguration.SetAppConfig("ComPortBaud", CB_ComPortBaudList.Text);

            ControlButtonText(BT_ConnPort, "Disconnect");
            Thread.Sleep(2000);
            // start thread
            m_CheckRunningThd = new Thread(new ThreadStart(thd_CheckReadThd));
            m_CheckRunningThd.Start();

            readyToReadData();
        }

        /* 
         * retrun 
         * -1: failed to get info
         * 0: user mark mode --> get info
         * 1: ecg mode -> skip
         * */
        int getDeviceInfo()
        {
            // send command to get device info
            UART_RX_CMD_T RxCmd = UART_RX_CMD_T.URX_CMD_GET_SYS_INFO;
            m_uartSendMsg[0] = (byte)RxCmd;

            cSerialPort.Clear();
            cSerialPort.Write(m_uartSendMsg, UART_RX_CHAR_LEN_MAX);

            Thread.Sleep(100);

            if (!m_isProductNameSet)
            {
                string str_tx = cSerialPort.ReadExisting();
                if (str_tx.Contains("[INFO] "))
                {
                    ParseDeviceInfo(str_tx);
                    TB_Serial_SetProductName();

                    m_curSerialName = m_productName[(int)PD_NAME_T.PD_NAME_SERIALNUM];
                    m_isProductNameSet = true;
                }
            }

            if (!m_isProductNameSet)
                return -1;

            return 0;
        }

        void readyToReadData()
        {
            UART_RX_CMD_T RxCmd = (UART_RX_CMD_T)0;
            string strFileExp = "";
            if (m_downMode == DOWN_MODE_T.DOWN_MODE_USRMARK)
            {
                RxCmd = UART_RX_CMD_T.URX_CMD_RD_NAND_USER_MARK;
                m_RdPageTotalCnt = (m_nandIdx_Ed[(int)DOWN_MODE_T.DOWN_MODE_USRMARK]
                            - m_nandIdx_St[(int)DOWN_MODE_T.DOWN_MODE_USRMARK] + 1);

                strFileExp = ".csv";

                // generates output file name
                string genTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                m_resFileName = genTime + "_" + m_productName[(int)PD_NAME_T.PD_NAME_SERIALNUM];
                m_resFilePath = m_curSavePath + "\\" + m_resFileName;
            }
            else if (m_downMode == DOWN_MODE_T.DOWN_MODE_ECGDATA)
            {
                RxCmd = UART_RX_CMD_T.URX_CMD_RD_NAND_ECG_DATA;
                m_RdPageTotalCnt = (m_nandIdx_Ed[(int)DOWN_MODE_T.DOWN_MODE_ECGDATA]
                            - m_nandIdx_St[(int)DOWN_MODE_T.DOWN_MODE_ECGDATA] + 1);

                strFileExp = ".bin";
            }

            // send command to get data
            m_uartSendMsg[0] = (byte)RxCmd;
            cSerialPort.Clear();
            cSerialPort.Write(m_uartSendMsg, UART_RX_CHAR_LEN_MAX);

            // set file path
            m_resFilePathExp = m_resFilePath + strFileExp;

            // ready to write file
            m_fs = new FileStream(m_resFilePathExp, FileMode.CreateNew, FileAccess.Write);
            m_bw = new BinaryWriter(m_fs);

            ControlTextBox(TB_LogMsg, "Downloading.. " + m_resFileName + strFileExp);

            // start thread
            m_RdSerialDataThd = new Thread(new ThreadStart(thd_Read));
            m_RdSerialDataThd.Start();
        }

        void thd_CheckReadThd()
        {
            while(true)
            {
                Thread.Sleep(500);
                Console.WriteLine(String.Format("TEST"));
                if (m_stopThd_1st != true)
                    continue;

                if (m_startThd_2nd == false)
                {
                    m_startThd_2nd = true;
                    readyToReadData();
                }
                if (m_stopThd_2nd == true)
                    break;
            }
            ControlTextBox(TB_LogMsg, "Succeed to download.: HEMP_" + m_curSerialName + Environment.NewLine);


            // ui: progress bar
            int val = 100;
            ControlProgressBar(progressBar1, val);
            ControlLabel(LB_ProgVal, val.ToString());
            
            // proc msg box for open web page
            if (MessageBox.Show(m_str_download_completed, m_str_download_completed_title, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (m_strCfg_uploadurl == "")
                {
                    MessageBox.Show(m_str_not_setup_url);
                }
                else
                {
                    System.Diagnostics.Process.Start(m_strCfg_uploadurl);
                }
            }

            // init flag
            m_isProductNameSet = false;
            m_stopThd_1st = false;
            m_startThd_2nd = false;
            m_stopThd_2nd = false;

            // ui
            setButtonEnabledUI(true);

            // close com port
            CloseSerial();
            // restore baudrate 115200
           
            timer_logout.Start();
        }

        void thd_Read()
        {
            cSerialPort.Clear();

            byte[] rBuf = new byte[MEM_PAGE_SZ];
            while (true)
            {
                int rCnt = cSerialPort.Read(rBuf, MEM_PAGE_SZ);
                if (rCnt < 0)
                {
                    Thread.Sleep(10);
                    Console.WriteLine(String.Format("err: {0}", rCnt));
                    continue;
                }

                Console.WriteLine(String.Format("r:{0}, w:{1}", rCnt, m_wrCnt));

                //
                m_bw.Write(rBuf, 0, MEM_PAGE_SZ);
                m_wrCnt += 1;
                Array.Clear(rBuf, 0, rCnt);

                // ui: progress bar
                if (m_wrCnt % 55 == 0)
                {
                    int val = 100 * m_wrCnt / m_RdPageTotalCnt;
                    ControlProgressBar(progressBar1, val);
                    ControlLabel(LB_ProgVal, val.ToString());
                }

                if (m_wrCnt == m_RdPageTotalCnt)
                    break;
            }
            m_wrCnt = 0;
            m_bw.Close();
            m_fs.Close();

            // convert result file
            if (m_downMode == DOWN_MODE_T.DOWN_MODE_USRMARK)
            {
                ExtractUserMarkFromFile(m_resFilePathExp);
                if (!m_dev)
                {
                    deleteFile(m_resFilePathExp, "_parse.csv");
                }

                m_downMode = DOWN_MODE_T.DOWN_MODE_ECGDATA;

                m_stopThd_1st = true;
            }
            else if (m_downMode == DOWN_MODE_T.DOWN_MODE_ECGDATA)
            {
                ConvertEcgData(m_resFilePathExp);
                if (!m_dev)
                {
                //    deleteFile(m_resFilePathExp, "_conv.bin");
                }
                m_downMode = DOWN_MODE_T.DOWN_MODE_USRMARK;

                m_stopThd_2nd = true;
            }
        }

        void deleteFile(string filePathExp, string tmpExp)
        {
            FileInfo fileDel = new FileInfo(filePathExp);
            if (fileDel.Exists) // 삭제할 파일이 있는지
            {
                fileDel.Delete(); // 없어도 에러안남
            }

            FileInfo fileRename = new FileInfo(filePathExp + tmpExp);
            if (fileRename.Exists)
            {
                fileRename.MoveTo(filePathExp); // 이미있으면 에러
            }
        }

        delegate void ctrl_Invoke_Button_Text(System.Windows.Forms.Button ctrl, string text); 
        public void ControlButtonText(System.Windows.Forms.Button ctr, string text)
        {
            if (ctr.InvokeRequired)
            {
                ctrl_Invoke_Button_Text CI = new ctrl_Invoke_Button_Text(ControlButtonText);
                ctr.Invoke(CI, ctr, text);
            }
            else
            {
                ctr.Text = text;
            }
        }

        delegate void ctrl_Invoke_Button(System.Windows.Forms.Button ctrl, bool enable);
        public void ControlButton(System.Windows.Forms.Button ctr, bool enable)
        {
            if (ctr.InvokeRequired)
            {
                ctrl_Invoke_Button CI = new ctrl_Invoke_Button(ControlButton);
                ctr.Invoke(CI, ctr, enable);
            }
            else
            {
                ctr.Enabled = enable;
            }
        }

        delegate void ctrl_Invoke_progressBar(System.Windows.Forms.ProgressBar ctrl, int val);
        public void ControlProgressBar(System.Windows.Forms.ProgressBar ctr, int val)
        {
            if (ctr.InvokeRequired)
            {
                ctrl_Invoke_progressBar CI = new ctrl_Invoke_progressBar(ControlProgressBar);
                ctr.Invoke(CI, ctr, val);
            }
            else
            {
                ctr.Value = val;
            }
        }

        delegate void ctrl_Invoke_Label(System.Windows.Forms.Label ctrl, string str);
        public void ControlLabel(System.Windows.Forms.Label ctr, string str)
        {
            if (ctr.InvokeRequired)
            {
                ctrl_Invoke_Label CI = new ctrl_Invoke_Label(ControlLabel);
                ctr.Invoke(CI, ctr, str);
            }
            else
            {
                string strVal = str + "%";
                ctr.Text = strVal;
            }
        }

        delegate void ctrl_Invoke(System.Windows.Forms.TextBox ctrl, string text);
        public void ControlTextBox(System.Windows.Forms.TextBox ctr, string textValue)
        {
            if (ctr.InvokeRequired)
            {
                ctrl_Invoke CI = new ctrl_Invoke(ControlTextBox);
                ctr.Invoke(CI, ctr, textValue);
            }
            else
            {
                string timeHeader = DateTime.Now.ToString("[yyyy-MM-dd.HH:mm:ss] ");
                ctr.Text = ctr.Text + timeHeader + textValue + Environment.NewLine;
                ctr.SelectionStart = ctr.Text.Length;
                ctr.ScrollToCaret();
            }

        }

        delegate void ctrl_Invoke_TextBox_Enable(System.Windows.Forms.TextBox ctrl, bool enable);
        public void ControlTextBox_Enable(System.Windows.Forms.TextBox ctr, bool bEnable)
        {
            if (ctr.InvokeRequired)
            {
                ctrl_Invoke_TextBox_Enable CI = new ctrl_Invoke_TextBox_Enable(ControlTextBox_Enable);
                ctr.Invoke(CI, ctr, bEnable);
            }
            else
            {
                ctr.Enabled = bEnable;
            }

        }

        private void CB_ComPortNameList_Click(object sender, EventArgs e)
        {
            RefreshComPortList();
        }

        private void ConvertEcgData(string tempFile)
        {
            byte[] d = File.ReadAllBytes(tempFile);
            int d_sz = d.Length;
            //FileStream stream = new FileStream(tempFile, FileMode.Open);

            FileStream fs1;
            BinaryWriter bw1;
            fs1 = new FileStream(tempFile + "_conv.bin", FileMode.Create, FileAccess.Write);
            bw1 = new BinaryWriter(fs1);

            byte[] pageData = new byte[MEM_PAGE_USE_SZ];
            bw1.Write(m_EcgHeader, 0, 32);

            // proc loop
            int procLoopCnt = d_sz / MEM_PAGE_SZ;

            // proc for pairing start page
            // 120 bytes packing 

            int readNandStartPage = m_nandIdx_St[(int)DOWN_MODE_T.DOWN_MODE_ECGDATA];
            int offset = 0;
            if (m_RdPageTotalCnt == 1)
                offset = 0;
            else
            {

            }



            /*
                      
                        int discardPageCnt = offset / MEM_PAGE_SZ;
                        int discardDataCnt = offset % MEM_PAGE_SZ;
                        if (discardDataCnt != 0)
                        {
                            int rdDataCnt = MEM_PAGE_USE_SZ - discardDataCnt;
                            Array.Copy(d, offset, pageData, 0, rdDataCnt);
                            bw1.Write(pageData, 0, rdDataCnt);
                            offset += (rdDataCnt+ (MEM_PAGE_SZ-MEM_PAGE_USE_SZ));

                            //
                            procLoopCnt -= 1;
                        }
                        procLoopCnt -= discardPageCnt;
            */

            // proc pages
            for (int i = 0; i < procLoopCnt; ++i)
            {
                Array.Copy(d, offset, pageData, 0, MEM_PAGE_USE_SZ);
                bw1.Write(pageData, 0, MEM_PAGE_USE_SZ);
                offset += MEM_PAGE_SZ;
            } 

            bw1.Close();
            fs1.Close();
        }

        private void ExtractUserMarkFromFile(string filePath)
        {
            byte[] fileData  = File.ReadAllBytes(filePath);

            StreamWriter fs_um = new StreamWriter(filePath + "_parse.csv");
            int pageCnt = fileData.Length / MEM_PAGE_SZ;
            int rCnt = 0;
            int pCnt = 0;

            // init
            string msg_line = "";
            m_pairStTime = -1;


            while (pCnt < pageCnt)
            {
                byte[] um = new byte[USER_MARK_SIZE];
                Array.Copy(fileData, rCnt, um, 0, USER_MARK_SIZE);

                if (um[0] == MEM_INIT_VAL && um[1] == MEM_INIT_VAL && um[2] == MEM_INIT_VAL && um[3] == MEM_INIT_VAL)
                    break;


                // parsing data
                int time = um[0] + (um[1] << 8) + (um[2] << 16) + (um[3] << 24) + (um[4] << 32);
                int update = um[15];
                int id = um[16] + (um[17] << 8) + (um[18] << 16) + (um[19] << 24);
                int page_idx = um[20] + (um[21] << 8) + (um[22] << 16) + (um[23] << 24);
                int page_pos = (um[24] + (um[25] << 8) + (um[26] << 16) + (um[27] << 24));
                USER_MARK_T type = (USER_MARK_T)(um[28]);

                // increase count
                rCnt += USER_MARK_SIZE;
                if (rCnt % MEM_PAGE_SZ == 0)
                    pCnt++;

                int pos = (page_idx - 64) * MEM_PAGE_SZ + page_pos;

                // get info. (pairing start)
                if (type== USER_MARK_T.USER_MARK_PAIRING_ST)
                {
                    if(m_pairStTime == -1)
                    {
                        // set device name byte for header
                        msg_line = System.Text.Encoding.UTF8.GetString(m_productNameByte);

                        m_pairStTime = time;
                        m_pairStPos = pos;
                        if (m_dev)
                        {
                            int posAdj_header = pos - m_pairStPos;
                            msg_line += String.Format(",{0},{1}, {2}" + Environment.NewLine, m_pairStTime, posAdj_header, pos);
                        }
                        else
                        {
                            msg_line += String.Format(",{0}" + Environment.NewLine, m_pairStTime);
                        }
                        fs_um.Write(msg_line);
                        msg_line = "";

                        Array.Copy(m_productNameByte, 0, m_EcgHeader, 0, 13);
                        Array.Copy(um, 0, m_EcgHeader, 0 + 16, 5);

                        //byte[] pos_byte = BitConverter.GetBytes(posAdj_header);
                        //Array.Copy(pos_byte, 0, m_EcgHeader, 5 + 16, 4);
                    }
                }
                if (m_pairStTime == -1)
                    continue;

                if (!m_dev)
                {
                    if (type != USER_MARK_T.USER_MARK_EVENT_BTN)
                    {
                        continue;
                    }
                }

                if (m_dev)
                {
                    int posAdj = pos - m_pairStPos;
                    msg_line += String.Format("{0},{1},{2},{3},{4},{5},{6}" + Environment.NewLine, id, (int)type, time, page_idx, page_pos, posAdj, update);
                    //Console.WriteLine(msg_line);
                }
                else
                {
                    msg_line += String.Format("{0},{1}" + Environment.NewLine, id, time);
                }
                fs_um.Write(msg_line);
                msg_line = "";
            }
            fs_um.Close();
            Console.WriteLine(String.Format("total {0} ", rCnt / USER_MARK_SIZE));
        }

        private void  addHeaderTest(String filePath)
        {
            byte[] fileData = File.ReadAllBytes(filePath);

            int rCnt = 0;
            byte[] header_um = new byte[USER_MARK_SIZE];
            while (rCnt < MEM_PAGE_SZ)
            {
                byte[] um = new byte[USER_MARK_SIZE];
                Array.Copy(fileData, rCnt, um, 0, USER_MARK_SIZE);

                if (um[0] == MEM_INIT_VAL && um[1] == MEM_INIT_VAL && um[2] == MEM_INIT_VAL && um[3] == MEM_INIT_VAL)
                    break;

                rCnt += USER_MARK_SIZE;

                int type = um[16] + (um[17] << 8) + (um[18] << 16) + (um[19] << 24);
                if(type==0)
                {
                    for( int i=0; i< USER_MARK_SIZE; ++i)
                    {
                        header_um[i] = 255;
                    }
                    
                    Array.Copy(m_productNameByte, 0, header_um, 0 , 13);
                    Array.Copy(um, 0, header_um, 0+16, 5);
                    Array.Copy(um, 24, header_um, 5+16, 4);
                    Array.Copy(um, 28, header_um, 9+16, 4);
                    break;
                }
            }


            //
            String ecgFile = filePath.Substring(0, filePath.Length-4) + ".bin_conv.bin";
            byte[] d = File.ReadAllBytes(ecgFile);
            int d_sz = d.Length;
            //FileStream stream = new FileStream(tempFile, FileMode.Open);

            FileStream fs1;
            BinaryWriter bw1;
            fs1 = new FileStream(ecgFile + "_header.bin", FileMode.Create, FileAccess.Write);
            bw1 = new BinaryWriter(fs1);

            bw1.Write(header_um, 0, USER_MARK_SIZE);
            int offset = 0 ;
            int loopCnt = d_sz / 4095;

            // loop until we can't read anymore
            for (int i = 0; i < loopCnt; ++i)
            {
                byte[] pageData4095 = new byte[4095];

                Array.Copy(d, offset, pageData4095, 0, 4095);

                bw1.Write(pageData4095, 0, 4095);
                offset += 4095;
            } // end while

            bw1.Close();
            fs1.Close();
        }

        private void main_window_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to quit?" , "Quit", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
                return;
            }

            if (cSerialPort.isConnected)
            {
                CloseSerial();
            }
        }

        private void BT_SelSaveDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                setSavePath(dialog.SelectedPath);
            }

        }

        private void BT_ClearLog_Click(object sender, EventArgs e)
        {
            TB_LogMsg.Clear();
        }

        private void BT_makeHeaderTest_Click(object sender, EventArgs e)
        {

            OpenFileDialog pFileDlg = new OpenFileDialog();
            pFileDlg.Filter = "Text Files(*.csv)|*.csv|All Files(*.*)|*.*";
            pFileDlg.Title = "Select ECG *.csv file";
            if (pFileDlg.ShowDialog() == DialogResult.OK)
            {
                String strFullPathFile = pFileDlg.FileName;
                addHeaderTest(strFullPathFile);
            }
        }

        private void BT_ExtractUmTest_Click(object sender, EventArgs e)
        {
            m_isProductNameSet = true;
            m_productNameByte = Encoding.UTF8.GetBytes("HPTT1AKR00039");

            OpenFileDialog pFileDlg = new OpenFileDialog();

            if (pFileDlg.ShowDialog() == DialogResult.OK)
            {
                String strFullPathFile = pFileDlg.FileName;
                ExtractUserMarkFromFile(strFullPathFile);
            }

        }

        private void BT_ConvEcgTest_Click(object sender, EventArgs e)
        {
            m_isProductNameSet = true;
            m_productNameByte = Encoding.UTF8.GetBytes("HPTT1AKR_12TT");
            Array.Copy(m_productNameByte, 0, m_EcgHeader, 0, 13);

            OpenFileDialog pFileDlg = new OpenFileDialog();
            pFileDlg.Filter = "Text Files(*.bin)|*.bin|All Files(*.*)|*.*";
            pFileDlg.Title = "Select ECG *.bin file";
            if (pFileDlg.ShowDialog() == DialogResult.OK)
            {
                String strFullPathFile = pFileDlg.FileName;

                ConvertEcgData(strFullPathFile);
            }
        }

        private void BT_LogOut_Click(object sender, EventArgs e)
        {
            login_window m_form_login = new login_window(); // Form2형 frm2 인스턴스화(객체 생성)
            m_form_login.LoginPass = false;
            m_form_login.ProgRunning = m_progRunning;
            this.Hide();
            m_form_login.ShowDialog();

            m_loginPass = m_form_login.LoginPass;
            if (!m_loginPass)
            {
                Application.ExitThread();
                Environment.Exit(0);
                return;
            }

            this.Show();

            //m_loginId = m_form_login.LoginId;
            //m_loginPw = m_form_login.LoginPw;
        }
    }
}
