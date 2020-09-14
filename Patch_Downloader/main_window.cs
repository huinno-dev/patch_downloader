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
        bool m_dev = true;
        Thread gReadSerialThd;

        string config_comport;
        string config_comportbaud;
        string config_savepath;
        public string config_uploadurl;

        private bool m_bDevMode = false;

        public main_window()
        {
            InitializeComponent();

            // Init val
            LB_ProgVal.Text = "";

            // config: comport
            config_comport = AppConfiguration.GetAppConfig("ComPortName");
            config_comportbaud = "3000000";
            //config_comportbaud = AppConfiguration.GetAppConfig("ComPortBaud");
            config_savepath = AppConfiguration.GetAppConfig("SavePath");
            config_uploadurl = AppConfiguration.GetAppConfig("UploadUrl");

            if(config_uploadurl=="")
            {
                MessageBox.Show("Please set url.");
            }
            // set save path
            if (config_savepath == "")
            {
                TB_SavePath.Text = Application.StartupPath + "\\Downloads";
                AppConfiguration.SetAppConfig("SavePath", TB_SavePath.Text);
            }
            else
            {
                TB_SavePath.Text = config_savepath;
            }

            // get comport name list
            RefreshComPortList();


            //// add event handler
            //this.KeyPreview = true;
            //this.KeyDown += new KeyEventHandler(main_window_KeyDown);

            //
            ShowDevMode(true);
        }

        private void ShowDevMode(bool isShow)
        {
            TB_Serial1.Visible = isShow;
            TB_Serial2.Visible = isShow;
            TB_Serial3.Visible = isShow;
            TB_Serial4.Visible = isShow;
            TB_Serial5.Visible = isShow;
            TB_Serial6.Visible = isShow;
        }

        private void RefreshComPortList()
        {
            string[] nameArray = cSerialPort.GetSerialComPortNameList();

            CB_ComPortNameList.Items.Clear();
            CB_ComPortNameList.Items.AddRange(nameArray);
            for (int i = 0; i < nameArray.Length; ++i)
            {
                if (nameArray[i] == config_comport)
                {
                    CB_ComPortNameList.Text = config_comport;
                    break;
                }
            }
            CB_ComPortBaudList.Text = config_comportbaud;
        }

        private void InitSerialTextBox( string a_huinno
                                        , string b_category
                                        , string c_assembleType
                                        , string d_version
                                        , string e_usageType
                                        , string f_country
                                        , string g_serialNum )
        {
            TB_Serial1.Text = a_huinno;
            TB_Serial2.Text = b_category;
            TB_Serial3.Text = c_assembleType;
            TB_Serial4.Text = d_version;
            TB_Serial5.Text = e_usageType;
            TB_Serial6.Text = f_country;
            TB_Serial7.Text = g_serialNum;
        }

        private void BT_OpenSavePath_Click(object sender, EventArgs e)
        {
            Process.Start(TB_SavePath.Text);
        }

        private void BT_SelSavePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                TB_SavePath.Text = dialog.SelectedPath + "\\Downloads";
                AppConfiguration.SetAppConfig("SavePath", TB_SavePath.Text);
            }
        }

        private void BT_ConnPort_Click(object sender, EventArgs e)
        {
            if (!cSerialPort.isConnected)
            {
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


                BT_ConnPort.Text = "Disconnect";
            }
            else
            {
                CloseSerial();
            }
        }

        private void CloseSerial()
        {
            cSerialPort.Close();
            BT_ConnPort.Text = "Connect";
        }

        const int UART_RX_CHAR_LEN_MAX = 16;
        byte[] sendMsg = new byte[UART_RX_CHAR_LEN_MAX];

        public enum UART_RX_CMD_T
        {
            URX_CMD_SET_PRODUCT_NAME = 0,        // 0
            URX_CMD_INIT_NAND_PAGE,         // 1
            URX_CMD_RD_NAND_ECG_DATA,        // 2
            URX_CMD_RD_NAND_USER_MARK,        // 3
            URX_CMD_SYSTEM_RESET,         // 4
            URX_CMD_GET_SYS_INFO,           // 5
            URX_CMD_RD_NAND_PAGE_ST_ED,     // 6
            UART_RX_CMD_NUM_MAX
        }

        const int UART_TX_CMD_MEM = 0x17;
        const int UART_TX_CMD_ECG = 0x38;
        const int UART_TX_CMD_LOG = 0xE8;

        bool isDevNameSet = false;

        string resFileName;
        FileStream fs;
        BinaryWriter bw;
        int wCnt = 0;

        string a_huinno         ;
        string b_category       ;
        string c_assembleType   ;
        string d_version        ;
        string e_usageType      ;
        string f_country        ;
        string g_serialNum;
        int nand0StIdx = 0;
        int nand0EdIdx = 0;
        int nand1StIdx = 0;
        int nand1EdIdx = 0;

        bool isCreateSaveDir = false;
        private void CreateSaveDir()
        {
            if (!isCreateSaveDir)
            {
                isCreateSaveDir = true;
                DirectoryInfo di = new DirectoryInfo(TB_SavePath.Text);
                if (di.Exists == false)
                {
                    di.Create();
                }
            }
        }

        private void ParseDeviceInfo(string str_tx)
        {
            int pos = str_tx.IndexOf("[INFO] ");
            pos += 7;

            a_huinno = str_tx.Substring(pos, 1); pos += 2;
            b_category = str_tx.Substring(pos, 2); pos += 3;
            c_assembleType = str_tx.Substring(pos, 1); pos += 2;
            d_version = str_tx.Substring(pos, 1); pos += 2;
            e_usageType = str_tx.Substring(pos, 1); pos += 2;
            f_country = str_tx.Substring(pos, 2); pos += 3;
            g_serialNum = str_tx.Substring(pos, 5); pos += 6;

            //
            string sub;
            sub = str_tx.Substring(pos, str_tx.Length - pos);
            if(m_dev) ControlTextBox(TB_LogMsg, sub);
            pos = sub.IndexOf(".");
            string str_0st_idx = sub.Substring(0, pos);

            sub = sub.Substring(pos + 1, sub.Length - pos - 1);
            pos = sub.IndexOf(".");
            string str_0ed_idx = sub.Substring(0, pos);

            sub = sub.Substring(pos + 1, sub.Length - pos - 1);
            pos = sub.IndexOf(".");
            string str_1st_idx = sub.Substring(0, pos);

            sub = sub.Substring(pos + 1, sub.Length - pos - 1);
            if (sub.Contains("?"))
            {
                int pos2 = sub.IndexOf("?");
                sub = sub.Substring(0, pos2);

            }
            string str_1ed_idx = sub;

            //
            nand0StIdx = Int32.Parse(str_0st_idx);
            nand0EdIdx = Int32.Parse(str_0ed_idx);
            nand1StIdx = Int32.Parse(str_1st_idx);
            nand1EdIdx = Int32.Parse(str_1ed_idx);
        }
        byte[] deviceName = new byte[13];

        string serialNum;
        int m_downMode = 0;
        string m_resFilePath;
        string m_resFilePathExp;

        private void BT_StartDown_Click(object sender, EventArgs e)
        {
            if (!cSerialPort.isConnected)
                return;

            // set ui
            ControlButton(BT_StartDown, false);
            ControlProgressBar(progressBar1, 0);

            // init value
            LB_ProgVal.Text = "";

            //
            CreateSaveDir();

            UART_RX_CMD_T RxCmd = (UART_RX_CMD_T )0;
            if (m_downMode == 0)
            {
                // send command to get device info
                RxCmd = UART_RX_CMD_T.URX_CMD_GET_SYS_INFO;
                sendMsg[0] = (byte)RxCmd;

                cSerialPort.Clear();
                cSerialPort.Write(sendMsg, UART_RX_CHAR_LEN_MAX);

                Thread.Sleep(100);

                if (!isDevNameSet)
                {
                    string str_tx = cSerialPort.ReadExisting();

                    if (str_tx.Contains("[INFO] "))
                    {
                        ParseDeviceInfo(str_tx);

                        InitSerialTextBox(
                            a_huinno
                            , b_category
                            , c_assembleType
                            , d_version
                            , e_usageType
                            , f_country
                            , g_serialNum
                            );

                        deviceName = Encoding.ASCII.GetBytes(a_huinno
                            + b_category 
                            + c_assembleType
                            + d_version
                            + e_usageType
                            + f_country
                            + g_serialNum );

                        serialNum = g_serialNum;
                        isDevNameSet = true;
                    }
                }

                if (!isDevNameSet)
                {
                    ControlTextBox(TB_LogMsg, "Failed to connect. Check COM port or reconnect patch to PC.");
                    ControlButton(BT_StartDown, true);
                    CloseSerial();
                    return;
                }
                ControlTextBox(TB_LogMsg, "Patch info.: HEMP_"+ g_serialNum);
            }
            ControlButton(BT_StartDown, false);

            // start thread
            gThreadCheckThd = new Thread(new ThreadStart(checkfirstDataDone));
            gThreadCheckThd.Start();

            readData();

        }

        string m_file_exp = "";
        void readData()
        {
            UART_RX_CMD_T RxCmd = (UART_RX_CMD_T)0;
            if (m_downMode == 0)
            {
                RxCmd = UART_RX_CMD_T.URX_CMD_RD_NAND_USER_MARK;
                total_len = (nand0EdIdx - nand0StIdx + 1);

                m_file_exp = ".csv";
            }
            else if (m_downMode == 1)
            {
                RxCmd = UART_RX_CMD_T.URX_CMD_RD_NAND_ECG_DATA;
                total_len = (nand1EdIdx - nand1StIdx + 1);

                m_file_exp = ".bin";
            }

            // send command to get data
            sendMsg[0] = (byte)RxCmd;

            cSerialPort.Clear();
            cSerialPort.Write(sendMsg, UART_RX_CHAR_LEN_MAX);

            if (m_downMode == 0 || m_downMode == 99)
            {
                // generates output
                string genTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                resFileName = genTime + "_" + serialNum;
            }

            // set file path
            m_resFilePath = TB_SavePath.Text + "\\" + resFileName;
            m_resFilePathExp = m_resFilePath + m_file_exp;

            fs = new FileStream(m_resFilePathExp, FileMode.CreateNew, FileAccess.Write);
            bw = new BinaryWriter(fs);

            ControlTextBox(TB_LogMsg, "Downloading.. " + resFileName + m_file_exp);

            // start thread
            gReadSerialThd = new Thread(new ThreadStart(readRun));
            gReadSerialThd.Start();
        }

        Thread gThreadCheckThd;
        bool m_start2ndData = false;
        void checkfirstDataDone()
        {
            while(true)
            {
                Thread.Sleep(500);
                Console.WriteLine(String.Format("TEST"));
                if (thread1_stop == 1)
                {
                    if (m_start2ndData == false)
                    {
                        m_start2ndData = true;
                        readData();
                    }
                    if (thread2_stop == 1)
                        break;
                }
            }
            if (MessageBox.Show("Link web page to upload data.", "Upload data", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                //MessageBox.Show("Yes");
                if (config_uploadurl == "")
                {
                    MessageBox.Show("Please set url.");
                }
                else
                {
                    System.Diagnostics.Process.Start(config_uploadurl);
                }
            }
            //else
            //{
            //    MessageBox.Show("아니요");
            //}
            m_start2ndData = false;
            isDevNameSet = false;
            thread1_stop = 0;
            thread2_stop = 0;

            //CloseSerial();
            ControlButton(BT_StartDown, true);
        }

        int thread1_stop = 0;
        int thread2_stop = 0;
        int total_len;

        static int rdBufSize = MEM_PAGE_SZ;

        void readRun()
        {
            byte[] rBuf = new byte[rdBufSize];
            while (true)
            {
                int rCnt = cSerialPort.Read(rBuf, rdBufSize);
                if (rCnt < 0)
                {
                    Thread.Sleep(10);
                    Console.WriteLine(String.Format("err: {0}", rCnt));
                    continue;
                }

                Console.WriteLine(String.Format("r:{0}, w:{1}", rCnt, wCnt));

                //
                bw.Write(rBuf, 0, rdBufSize);
                wCnt += 1;
                Array.Clear(rBuf, 0, rCnt);

                if (wCnt % 55 == 0)
                {
                    int val = 100 * wCnt / total_len;
                    ControlProgressBar(progressBar1, val);
                    ControlLabel(LB_ProgVal, val.ToString());
                }

                if (wCnt == total_len)
                {
                    int val = 100;
                    ControlProgressBar(progressBar1, val);
                    ControlLabel(LB_ProgVal, val.ToString());
                    break;
                }
            }
            wCnt = 0;
            bw.Close();
            fs.Close();
            if (m_downMode == 1)
            {
                ConvertData(m_resFilePathExp);
                m_downMode = 0;

                ControlTextBox(TB_LogMsg, "Succeed to download.: HEMP_" + g_serialNum+ Environment.NewLine);
                thread2_stop = 1;

                if (!m_dev)
                {
                    FileInfo fileDel = new FileInfo(m_resFilePathExp);
                    if (fileDel.Exists) // 삭제할 파일이 있는지
                    {
                        fileDel.Delete(); // 없어도 에러안남
                    }

                    FileInfo fileRename = new FileInfo(m_resFilePathExp + "_conv.bin");
                    if (fileRename.Exists)
                    {
                        fileRename.MoveTo(m_resFilePathExp); // 이미있으면 에러
                    }
                }
            }
            else if (m_downMode == 0)
            {
                ExtractUserMarkFromFile(m_resFilePathExp);
                m_downMode = 1;
                thread1_stop = 1;

                if (!m_dev)
                {
                    FileInfo fileDel = new FileInfo(m_resFilePathExp);
                    if (fileDel.Exists) // 삭제할 파일이 있는지
                    {
                        fileDel.Delete(); // 없어도 에러안남
                    }

                    FileInfo fileRename = new FileInfo(m_resFilePathExp + "_parse.csv");
                    if (fileRename.Exists)
                    {
                        fileRename.MoveTo(m_resFilePathExp); // 이미있으면 에러
                    }
                }                
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

        private void CB_ComPortNameList_Click(object sender, EventArgs e)
        {
            RefreshComPortList();
        }

        private void BT_ConvUserMark_Click(object sender, EventArgs e)
        {
            OpenFileDialog pFileDlg = new OpenFileDialog();
            pFileDlg.Filter = "Text Files(*.bin)|*.bin|All Files(*.*)|*.*";
            pFileDlg.Title = "Select ECG *.bin file";
            if (pFileDlg.ShowDialog() == DialogResult.OK)
            {
                String strFullPathFile = pFileDlg.FileName;
                //ConvertData_4095(strFullPathFile);
                ConvertData(strFullPathFile);
            }
        }

        const int MEM_4G_PAGE_FULL_SZ = 4096;
        const int MEM_4G_PAGE_USE_SZ = 4095;
        const int MEM_2G_PAGE_FULL_SZ = 2048;
        const int MEM_2G_PAGE_USE_SZ = 2046;

        //const int MEM_PAGE_SZ = MEM_4G_PAGE_FULL_SZ;
        //const int MEM_PAGE_USE_SZ = MEM_4G_PAGE_USE_SZ;
        const int MEM_PAGE_SZ = MEM_2G_PAGE_FULL_SZ;
        const int MEM_PAGE_USE_SZ = MEM_2G_PAGE_USE_SZ;

        private void ConvertData(string tempFile)
        {
            byte[] d = File.ReadAllBytes(tempFile);
            int d_sz = d.Length;
            //FileStream stream = new FileStream(tempFile, FileMode.Open);

            FileStream fs1;
            BinaryWriter bw1;
            fs1 = new FileStream(tempFile + "_conv.bin", FileMode.Create, FileAccess.Write);
            bw1 = new BinaryWriter(fs1);

            int offset = 0;
            int remainCnt = d_sz - offset;
            int loopCnt = remainCnt / MEM_PAGE_SZ;
            byte[] pageData = new byte[MEM_PAGE_USE_SZ];
                bw1.Write(m_EcgHeader, 0, 32);
            // loop until we can't read any more
            for (int i = 0; i < loopCnt; ++i)
            {
                Array.Copy(d, offset, pageData, 0, MEM_PAGE_USE_SZ);
                bw1.Write(pageData, 0, MEM_PAGE_USE_SZ);

                offset += MEM_PAGE_SZ;
            } // end while

            bw1.Close();
            fs1.Close();
        }

        const int PAGE_SIZE = 2048;
        const int USER_MARK_SIZE = 32;
        const int INIT_VAL = 0xFF;

        public enum USER_MARK_T
        {
            USER_MARK_PAIRING_ST = 0,        // 0
            USER_MARK_PAIRING_ED,         // 1
            USER_MARK_EVENT_APP,        // 2
            USER_MARK_EVENT_BTN,        // 3
            USER_MARK_TYPE_NUM_MAX
        }

        byte[] m_EcgHeader = new byte[32];

        private void ExtractUserMarkFromFile(string filePath)
        {
            byte[] fileData  = File.ReadAllBytes(filePath);

            StreamWriter fs_um = new StreamWriter(filePath + "_parse.csv");
            int pageCnt = fileData.Length / PAGE_SIZE;
            int rCnt = 0;
            int pCnt = 0;
            int start_time = -1;
            string msg_line = "";

            if (isDevNameSet)
            {
                msg_line = System.Text.Encoding.UTF8.GetString(deviceName);
            }
            while (pCnt < pageCnt)
            {
                byte[] um = new byte[USER_MARK_SIZE];
                Array.Copy(fileData, rCnt, um, 0, USER_MARK_SIZE);

                if (um[0] == INIT_VAL && um[1] == INIT_VAL && um[2] == INIT_VAL && um[3] == INIT_VAL)
                    break;

                rCnt += USER_MARK_SIZE;
                if (rCnt % PAGE_SIZE == 0)
                    pCnt++;

                int time = 0;
                int update = 0;
                int id = 0;
                int page_idx = 0;
                int page_pos = 0;
                USER_MARK_T type = USER_MARK_T.USER_MARK_TYPE_NUM_MAX;

                // parsing data
                if (MEM_PAGE_SZ == MEM_2G_PAGE_FULL_SZ)
                {
                    time = um[0] + (um[1] << 8) + (um[2] << 16) + (um[3] << 24) + (um[4] << 32);
                    update = um[15];
                    id = um[16] + (um[17] << 8) + (um[18] << 16) + (um[19] << 24);
                    page_idx = um[20] + (um[21] << 8) + (um[22] << 16) + (um[23] << 24);
                    page_pos = (um[24] + (um[25] << 8) + (um[26] << 16) + (um[27] << 24));
                    type = (USER_MARK_T)(um[28]);
                }
                else
                {
                    time = um[0] + (um[1] << 8) + (um[2] << 16) + (um[3] << 24) + (um[4] << 32);
                    update = um[15];
                    type = (USER_MARK_T)(um[16] + (um[17] << 8) + (um[18] << 16) + (um[19] << 24));
                    id = um[20] + (um[21] << 8) + (um[22] << 16) + (um[23] << 24);
                    page_idx = (um[24] + (um[25] << 8) + (um[26] << 16) + (um[27] << 24));
                    page_pos = (um[28] + (um[29] << 8) + (um[30] << 16));
                }
                int pos = (page_idx - 64) * MEM_PAGE_USE_SZ + page_pos;
                pos = (pos < 0) ? 0 : pos;
                byte[] pos_byte = BitConverter.GetBytes(pos);

                // get info. (pairing start)
                if (type== USER_MARK_T.USER_MARK_PAIRING_ST)
                {
                    if(start_time==-1)
                    {
                        start_time = time;
                        msg_line += String.Format(",{0},{1}" + Environment.NewLine, start_time, pos);
                        fs_um.Write(msg_line);
                        msg_line = "";
                                                
                        Array.Copy(deviceName, 0, m_EcgHeader, 0, 13);
                        Array.Copy(um, 0, m_EcgHeader, 0 + 16, 5);
                        Array.Copy(pos_byte, 0, m_EcgHeader, 5 + 16, 4);
                    }
                }

                if (start_time == -1)
                    continue;

                if (type != USER_MARK_T.USER_MARK_EVENT_BTN)
                {
                    continue;
                }

                if (m_dev)
                {
                    msg_line += String.Format("{0},{1},{2},{3},{4},{5},{6}" + Environment.NewLine, id, (int)type, time, page_idx, page_pos, pos, update);
                    //Console.WriteLine(msg_line);
                }
                else
                {
                    msg_line += String.Format("{0},{1},{2}" + Environment.NewLine, id, time, pos);
                }
                fs_um.Write(msg_line);
                msg_line = "";
            }
            fs_um.Close();
            Console.WriteLine(String.Format("total {0} ", rCnt / USER_MARK_SIZE));
        }


        private void main_window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D && (ModifierKeys & (Keys.Control| Keys.ShiftKey) )== Keys.Control)
            {
                // CTRL + UP was pressed
                win_password frm2 = new win_password();
                frm2.PassStr = config_uploadurl;
                frm2.ShowDialog();

                m_bDevMode = frm2.Passvalue;

                if(m_bDevMode)
                {

                    //
                    ShowDevMode(m_bDevMode);
                    config_uploadurl = frm2.PassStr;
                    AppConfiguration.SetAppConfig("UploadUrl", config_uploadurl);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            isDevNameSet = true;
            //deviceName = Encoding.UTF8.GetBytes("TEST_________");
            deviceName = Encoding.UTF8.GetBytes("HPTT1AKR00039");

            OpenFileDialog pFileDlg = new OpenFileDialog();
            //pFileDlg.Filter = "Text Files(*.bin)|*.bin|All Files(*.*)|*.*";
            //pFileDlg.Title = "Select ECG *.bin file";
            if (pFileDlg.ShowDialog() == DialogResult.OK)
            {
                String strFullPathFile = pFileDlg.FileName;
                ExtractUserMarkFromFile(strFullPathFile);
            }

        }

        int m_drawDataCnt = 0;
        const int DRAW_DATA_NUM = 3*2;
        double[] m_drawData = new double[DRAW_DATA_NUM];


        delegate void ctrl_Invoke3(System.Windows.Forms.DataVisualization.Charting.Chart ctrl, double[] val);
        public void drawSeries(System.Windows.Forms.DataVisualization.Charting.Chart ctr, double [] val)
        {
            try
            {
                if (ctr.InvokeRequired)
                {
                    ctrl_Invoke3 CI = new ctrl_Invoke3(drawSeries);
                    // if (readThreadActive == false) return;
                    ctr.Invoke(CI, ctr, val);

                }
                else
                {
                    m_drawData[m_drawDataCnt+0] = val[0];
                    m_drawData[m_drawDataCnt+1] = val[1];
                    m_drawDataCnt += 2;

                    if (m_drawDataCnt < DRAW_DATA_NUM)
                        return;

                    m_drawDataCnt = 0;


                    //if (insPos % drawRate == 0)
                    //{
                    //    drawArray(ctr, insPos, 1);
                    //    Thread.Sleep(drawInterval);
                    //}
                    //        insPos = (insPos + 1) % SIG_LEN;
                }
            }
            catch (System.ComponentModel.InvalidAsynchronousStateException exception)
            {
            }
            catch (Exception exception)
            {
            }
        }
        public void drawArray(System.Windows.Forms.DataVisualization.Charting.Chart ctr, UInt32 drawSPos, UInt32 updateSize)
        {
            //double ymin = 10000;
            //double ymax = -10000;
            ////if (drawSPos % 4 != 0) return;
            //List<UInt32> index = new List<UInt32>();


            //UInt32 tickPos = sigTickInitPos + drawSPos;

            //if (sigTickInitPos == 0)
            //{
            //    gausianData[0] = adcDrawArray[0];
            //    gausianData[1] = adcDrawArray[0];
            //}

            //for (UInt32 i = 0; i < drawSPos; i++)
            //{
            //    tickArray[i] = i + totoalTick;
            //    shiftArray[i] = adcDrawArray[i];

            //    if (ymin > shiftArray[i]) ymin = shiftArray[i];
            //    if (ymax < shiftArray[i]) ymax = shiftArray[i];
            //}


            //double ymean = (ymin + ymax) / 2;
            //for (UInt32 i = drawSPos; i < SIG_LEN; i++)
            //{
            //    tickArray[i] = i + totoalTick;
            //    shiftArray[i] = ymean;

            //    if (ymin > shiftArray[i]) ymin = shiftArray[i];
            //}

            //ctr.ChartAreas[0].AxisY.IsStartedFromZero = false;
            ////      ctr.ChartAreas[0].AxisY.Maximum = ymax;
            //ctr.ChartAreas[0].AxisY.Minimum = ymin - 1;

            ////Array.Copy(adcDrawArray, 0, shiftArray, 0, SIG_LEN );
            ////Array.Reverse(shiftArray);
            //if (ctr.Series.Count == 0)
            //{
            //    adcCurve = ctr.Series.Add("adcCurve"); //새로운 series 생성

            //    adcCurve.BorderWidth = 1;
            //    adcCurve.ChartType = SeriesChartType.Line; //그래프 모양을 '선'으로 지정
            //    adcCurve.Points.DataBindXY(tickArray, shiftArray);
            //}
            //else
            //{
            //    adcCurve.Points.DataBindXY(tickArray, shiftArray);
            //    //      ctr.Series.Clear();
            //    //      adcCurve = ctr.Series.Add("adcCurve"); //새로운 series 생성

            //    //     adcCurve.BorderWidth = 1;
            //    //      adcCurve.ChartType = SeriesChartType.Line; //그래프 모양을 '선'으로 지정
            //    //     adcCurve.Points.DataBindXY(tickArray, shiftArray);
            //}

            //Invalidate();
            //Update();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog pFileDlg = new OpenFileDialog();
            pFileDlg.Filter = "Text Files(*.csv)|*.csv|All Files(*.*)|*.*";
            pFileDlg.Title = "Select ECG *.csv file";
            if (pFileDlg.ShowDialog() == DialogResult.OK)
            {
                String strFullPathFile = pFileDlg.FileName;
                addHeader(strFullPathFile);
            }
        }
        private void  addHeader(String filePath)
        {
            byte[] fileData = File.ReadAllBytes(filePath);

            int rCnt = 0;
            byte[] header_um = new byte[USER_MARK_SIZE];
            while (rCnt < PAGE_SIZE)
            {
                byte[] um = new byte[USER_MARK_SIZE];
                Array.Copy(fileData, rCnt, um, 0, USER_MARK_SIZE);

                if (um[0] == INIT_VAL && um[1] == INIT_VAL && um[2] == INIT_VAL && um[3] == INIT_VAL)
                    break;

                rCnt += USER_MARK_SIZE;

                int type = um[16] + (um[17] << 8) + (um[18] << 16) + (um[19] << 24);
                if(type==0)
                {
                    for( int i=0; i< USER_MARK_SIZE; ++i)
                    {
                        header_um[i] = 255;
                    }
                    
                    Array.Copy(deviceName, 0, header_um, 0 , 13);
                    Array.Copy(um, 0, header_um, 0+16, 5);
                    Array.Copy(um, 24, header_um, 5+16, 4);
                    Array.Copy(um, 28, header_um, 9+16, 4);
                    //int time = um[0] + (um[1] << 8) + (um[2] << 16) + (um[3] << 24) + (um[4] << 32);
                    //int update = um[15];
                    //int type = um[16] + (um[17] << 8) + (um[18] << 16) + (um[19] << 24);
                    //int id = um[20] + (um[21] << 8) + (um[22] << 16) + (um[23] << 24);
                    //int page_idx = um[24] + (um[25] << 8) + (um[26] << 16) + (um[27] << 24);
                    //int page_pos = um[28] + (um[29] << 8) + (um[30] << 16);
                    //Array.Copy(um, 0, header_um, 0, USER_MARK_SIZE);
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
            // loop until we can't read any more
            for (int i = 0; i < loopCnt; ++i)
            {
                byte[] pageData4095 = new byte[4095];

                Array.Copy(d, offset, pageData4095, 0, 4095);

                bw1.Write(pageData4095, 0, 4095);
                offset += 4095;
            } // end while

            //stream.Close();

            bw1.Close();
            fs1.Close();
        }

        private void main_window_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (cSerialPort.isConnected)
            {
                CloseSerial();
            }
        }
    }
}
