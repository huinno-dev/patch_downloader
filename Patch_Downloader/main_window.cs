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

namespace Huinno_Downloader
{
    public partial class main_window : Form
    {
        Thread gReadSerialThd;

        string config_comport;
        string config_comportbaud;
        string config_savepath;

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

            // set save path
            if (config_savepath == "")
            {
                TB_SavePath.Text = Application.StartupPath + "\\Downloads";
            }

            // get comport name list
            RefreshComPortList();


            // add event handler
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(main_window_KeyDown);

            //
            ShowDevMode(m_bDevMode);
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
                TB_SavePath.Text = dialog.SelectedPath + "\\Downloads";

            // CommonOpenFileDialog 클래스 생성 
            //CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            //// 처음 보여줄 폴더 설정(안해도 됨) 
            ////dialog.InitialDirectory = "";
            ////dialog.IsFolderPicker = true;
            ////dialog.
            //if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            //{
            //    TB_SavePath.Text = dialog.FileName;
            //    // 테스트용, 폴더 선택이 완료되면 선택된 폴더를 label에 출력
            //}

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
                DirectoryInfo di = new DirectoryInfo(BT_ConnPort.Text);
                if (di.Exists == false)
                {
                    di.Create();
                }
            }
        }

        private void ParseDeviceInfo(string str_tx)
        {
            ControlTextBox(TB_LogMsg, "Load device info.");

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
            ControlTextBox(TB_LogMsg, sub);
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

        private void BT_StartDown_Click(object sender, EventArgs e)
        {
            if (!cSerialPort.isConnected)
                return;

            // set ui
            ControlButton(BT_StartDown, false);

            // init value
            LB_ProgVal.Text = "";

            //
            CreateSaveDir();

            // send command to get device info
            UART_RX_CMD_T RxCmd;
            RxCmd = UART_RX_CMD_T.URX_CMD_GET_SYS_INFO;
            sendMsg[0] = (byte)RxCmd;

            cSerialPort.Clear();
            cSerialPort.Write(sendMsg, UART_RX_CHAR_LEN_MAX);

            Thread.Sleep(500);
            
            string serialNum = "UNKNOWN";
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

                    serialNum = g_serialNum;
                    isDevNameSet = true;
                }
            }

            if (!isDevNameSet)
            {
                ControlTextBox(TB_LogMsg, "Try again after few sencond.");
                ControlButton(BT_StartDown, true);
                return;
            }
            ControlButton(BT_StartDown, false);

            //
            total_len = (nand1EdIdx - nand1StIdx + 1);

            // send command to get data
            RxCmd = UART_RX_CMD_T.URX_CMD_RD_NAND_USER_MARK;
            //RxCmd = UART_RX_CMD_T.URX_CMD_RD_NAND_ECG_DATA;
            sendMsg[0] = (byte)RxCmd;

            cSerialPort.Clear();
            cSerialPort.Write(sendMsg, UART_RX_CHAR_LEN_MAX);

            // generates output
            string genTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            resFileName = genTime + "_" + serialNum + ".bin";

            string filePath = TB_SavePath.Text + "\\" + resFileName;
            fs = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write);
            bw = new BinaryWriter(fs);

            ControlTextBox(TB_LogMsg, "START");
            // start thread
            gReadSerialThd = new Thread(new ThreadStart(readRun));
            gReadSerialThd.Start();
        }

        int total_len;
        void readRun()
        {
            byte[] rBuf = new byte[1024*4*2];
            while (true)
            {
                int rCnt = cSerialPort.Read(rBuf);
                if (rCnt < 0)
                {
                    //if (rCnt == -1) break;

                    Thread.Sleep(10);
                    Console.WriteLine(String.Format("err: {0}", rCnt));
                    continue;
                }

                Console.WriteLine(String.Format("r:{0}, w:{1}", rCnt, wCnt));

                //
                bw.Write(rBuf, 0, 1024*4);
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
            ControlTextBox(TB_LogMsg, "End: " + resFileName);
            isDevNameSet = false;
            ControlButton(BT_StartDown, true);

            if (MessageBox.Show("서버 업로드", "YesOrNo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                MessageBox.Show("예 클릭");
            }
            else
            {
                MessageBox.Show("아니요 클릭");
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

            string filePath1 = "C:\\binna\\git\\patch_downloader\\Patch_Downloader\\bin\\Debug\\Downloads\\2020-08-04_18-42-37_UNKNOWN.bin";
            string filePath2 = "C:\\binna\\git\\patch_downloader\\Patch_Downloader\\bin\\Debug\\Downloads\\2020-08-04_16-53-09_UNKNOWN.bin";
            ExtractUserMarkFromFile(filePath1);
            ExtractEcgDataFromFile(filePath2);
        }

        private void ExtractEcgDataFromFile(string tempFile)
        {
            FileStream stream = new FileStream(tempFile, FileMode.Open);

            // loop until we can't read any more
            while (true)
            {
                // All ints are 4-bytes
                byte[] pageData = new byte[4096];
                // Read size
                int numRead = stream.Read(pageData, 0, 4096);
                if (numRead <= 0)
                {
                    break;
                }
            } // end while

            stream.Close();
        }


        const int PAGE_SIZE = 4096;
        const int USER_MARK_SIZE = 256;

        private void ExtractUserMarkFromFile(string filePath)
        {
            //byte[] fileData = File.ReadAllBytes(filePath);

            byte[] d = File.ReadAllBytes(filePath);

            byte[] fileData = new byte[4096];

            Array.Copy(d, 20, fileData, 0, 4096 - 20);

            int rCnt = 0;
            while (rCnt < PAGE_SIZE)
            {
                byte[] um = new byte[USER_MARK_SIZE];
                Array.Copy(fileData, rCnt, um, 0, 256);

                if (um[0] == 0xff)
                    break;

                rCnt += 256;

                int year = um[0] * 100 + um[1];
                int mon = um[2];
                int day = um[3];
                int time = um[4] * 256 * 256 + um[5] * 256 + um[6];
                int type = um[243];
                int id = (um[247] << 24) + (um[246] << 16) + (um[245] << 8) + um[244];
                int page_idx = (um[251] << 24) + (um[250] << 16) + (um[249] << 8) + um[248];
                Console.WriteLine(String.Format("{0} {1} {2} {3} {4} {5} {6} ", year, mon, day, time, type, id, page_idx));
                int t_hour = time / 3600;
                int t_min = (time - t_hour * 3600) / 60;
                int t_sec = (time - t_hour * 3600 - t_min * 60);
                Console.WriteLine(String.Format("{0}:{1}:{2}  ", t_hour, t_min, t_sec));
            }
            Console.WriteLine(String.Format("total {0} ", rCnt / 256));
        }


        private void main_window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D && (ModifierKeys & (Keys.Control| Keys.ShiftKey) )== Keys.Control)
            {
                // CTRL + UP was pressed
                win_password frm2 = new win_password();
                frm2.ShowDialog();

                m_bDevMode = frm2.Passvalue;

                if(m_bDevMode)
                {

                    //
                    ShowDevMode(m_bDevMode);
                }
            }
        }
    }
}
