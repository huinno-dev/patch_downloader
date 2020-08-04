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

namespace Huinno_Downloader
{
    public partial class main_window : Form
    {
        Thread gReadSerialThd;

        //byte[] rBuf;

        public main_window()
        {
            InitializeComponent();

            // get comport name list
            string[] nameArray = cSerialPort.GetSerialComPortNameList();

            // config: comport
            string config_comport = AppConfiguration.GetAppConfig("ComPortName");
            CB_ComPortNameList.Items.AddRange(nameArray);
            for (int i = 0; i < nameArray.Length; ++i)
            {
                if (nameArray[i] == config_comport)
                {
                    CB_ComPortNameList.Text = config_comport;
                    break;
                }
            }
            string config_comportbaud = AppConfiguration.GetAppConfig("ComPortBaud");
            CB_ComPortBaudList.Text = config_comportbaud;


            // config: save path
            string config_savepath = AppConfiguration.GetAppConfig("SavePath");
            if (config_savepath == "")
            {
                string sDirPath;
                sDirPath = Application.StartupPath + "\\Downloads";
                DirectoryInfo di = new DirectoryInfo(sDirPath);
                if (di.Exists == false)
                {
                    di.Create();
                }
                TB_SavePath.Text = sDirPath;
            }

            //rBuf = new byte[PACKETSIZE * 4 * 4];
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

        }

        private void BT_SelSavePath_Click(object sender, EventArgs e)
        {

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
                                            //    RD_USER_MARK,         // 5
                                            //    RESET_USER_MARK,      // 6
                                            //    ECG_VIEW_START,       // 7
                                            //    ECG_VIEW_END,         // 8
                                            //    FW_UPGRADE,           // 9
                                            //    WR_USER_SN,           // 10
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
        private void BT_StartDown_Click(object sender, EventArgs e)
        {
            if (!cSerialPort.isConnected)
                return;

            BT_StartDown.Enabled = false;

            UART_RX_CMD_T RxCmd;

            cSerialPort.Clear();
            RxCmd = UART_RX_CMD_T.URX_CMD_GET_SYS_INFO;
            sendMsg[0] = (byte)RxCmd;
            cSerialPort.Write(sendMsg, UART_RX_CHAR_LEN_MAX);

            Thread.Sleep(500);
            
            string serialNum = "UNKNOWN";
            if (!isDevNameSet)
            {
                string str_tx = cSerialPort.ReadExisting();

                if (str_tx.Contains("[INFO] "))
                {
                    ControlTextBox(TB_LogMsg, "Load device info.");

                    int pos = str_tx.IndexOf("[INFO] ");
                    pos += 7;

                    a_huinno         = str_tx.Substring(pos, 1); pos += 2;
                    b_category       = str_tx.Substring(pos, 2); pos += 3;
                    c_assembleType   = str_tx.Substring(pos, 1); pos += 2;
                    d_version        = str_tx.Substring(pos, 1); pos += 2;
                    e_usageType      = str_tx.Substring(pos, 1); pos += 2;
                    f_country        = str_tx.Substring(pos, 2); pos += 3;
                    g_serialNum      = str_tx.Substring(pos, 5); pos += 6;

                    string sub;
                    sub = str_tx.Substring(pos, str_tx.Length-pos);
                    ControlTextBox(TB_LogMsg, "Start: "+ sub);
                    pos = sub.IndexOf(".");
                    string str_0st_idx = sub.Substring(0, pos);

                    sub = sub.Substring(pos+1, sub.Length - pos-1);
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
                    nand0StIdx = Int32.Parse(str_0st_idx);
                    nand0EdIdx = Int32.Parse(str_0ed_idx);
                    nand1StIdx = Int32.Parse(str_1st_idx);
                    nand1EdIdx = Int32.Parse(str_1ed_idx);


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

                //if (str.Contains("BLE device name: "))
                //{
                //    int st = str.IndexOf("BLE device name: ");
                //    st += 17;

                //    string bt_name = str.Substring(st, 10);
                //    ControlTextBox(TB_LogMsg, "BLE device name: " + bt_name);

                //    serialNum = str.Substring(st + 5, 5);
                //    InitSerialTextBox(serialNum);

                //    isDevNameSet = true;
                //}

            }

            //return;

            if (!isDevNameSet)
            {

                ControlTextBox(TB_LogMsg, "Try Again.");
                BT_StartDown.Enabled = true;
                return;
            }

            BT_StartDown.Enabled = false;

            //RxCmd = UART_RX_CMD_T.URX_CMD_RD_NAND_USER_MARK;
            RxCmd = UART_RX_CMD_T.URX_CMD_RD_NAND_ECG_DATA;
            sendMsg[0] = (byte)RxCmd;

            cSerialPort.Clear();
            cSerialPort.Write(sendMsg, UART_RX_CHAR_LEN_MAX);

            // generates output
            string genTime = DateTime.Now.ToString("yyyyMMdd_HH-mm-ss");
            resFileName = "result_" + serialNum + "_" + genTime + ".bin";

            fs = new FileStream(resFileName, FileMode.CreateNew, FileAccess.Write);
            bw = new BinaryWriter(fs);
            //ReadBytes += 1024;
            //bw.Write(rBuffer, 0, 1024);
            total_len = (nand1EdIdx - nand1StIdx + 1);

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
                    Thread.Sleep(10);
                    Console.WriteLine(String.Format("err: {0}", rCnt));
                    continue;
                }

                Console.WriteLine(String.Format("r:{0}, w:{1}", rCnt, wCnt));
#if (USE0)
                switch (rBuf[0])
                {
                    case UART_TX_CMD_MEM:  // cmd  - Nand Data
                        ControlTextBox(TB_LogMsg, rCnt.ToString());
                        //saveECGNandData();
                        break;
#if (USE_ECG_DATA)
                                    //case UART_TX_CMD_ECG:  // cmd - ECG Data
                                    //    /*
                                    //            if (veryIdx == -1)
                                    //                veryIdx = rBuffer[1];
                                    //            else
                                    //            {
                                    //                if(rBuffer[1] != (veryIdx+ 1)%256)
                                    //                {
                                    //                    int error = 1;
                                    //                }
                                    //            }
                                    //            setText_Control(TextBox_System_Log, "txIdx" + rBuffer[1].ToString() + Environment.NewLine);
                                    //    */

                                    //    if (RADIO_ECG_24B.Checked == true)
                                    //    {
                                    //        netDataLen = rBuffer[2] - 6;
                                    //        dataStartOffset = 6;

                                    //    }
                                    //    else if (RADIO_ECG_16B.Checked == true)
                                    //    {
                                    //        netDataLen = rBuffer[2] - 4;
                                    //        dataStartOffset = 4;
                                    //    }
                                    //    else if (RADIO_ECG_12B.Checked == true)
                                    //    {
                                    //        netDataLen = rBuffer[2] - 6;
                                    //        dataStartOffset = 6;
                                    //    }
                                    //    else
                                    //    {
                                    //        netDataLen = rBuffer[2];
                                    //    }
                                    //    BufLen = (UInt16)netDataLen;
                                    //    totalRevEcgBytes += netDataLen;
                                    //    // if (RADIO_COMPRESSED.Checked == true)
                                    //    //     verifyNandBuf();
                                    //    if (nandWOffset + netDataLen < nandPageBuf.Length)
                                    //    {
                                    //        Array.Copy(rBuffer, 3 + dataStartOffset, nandPageBuf, nandWOffset, netDataLen);
                                    //        nandWOffset += netDataLen;
                                    //    }
                                    //    else
                                    //    {

                                    //        int r = nandWOffset + netDataLen - nandPageBuf.Length;
                                    //        int h = netDataLen - r;
                                    //        Array.Copy(rBuffer, 3 + dataStartOffset, nandPageBuf, nandWOffset, h);
                                    //        Array.Copy(rBuffer, 3 + dataStartOffset + h, nandPageBuf, 0, r);
                                    //        nandWOffset = r;
                                    //    }
                                    //    //setText_Control(TextBox_System_Log, "Comm round:" + commNum.ToString() + Environment.NewLine);
                                    //    commNum++;

                                    //break;
#endif //USE_ECG_DATA
                    case 0xE8:  // log
                        string result = System.Text.Encoding.UTF8.GetString(rBuf, 3, rBuf[2]);
                        ControlTextBox(TB_LogMsg, result);

                        break;

                }
#endif 
                bw.Write(rBuf, 0, 1024*4);
                wCnt += 1;
                Array.Clear(rBuf, 0, rCnt);

                if (wCnt % 50 == 0)
                    Progress(wCnt);

                if (wCnt == total_len)
                {
                    break;
                }
            }
            wCnt = 0;
            bw.Close();
            fs.Close();
            ControlTextBox(TB_LogMsg, "End: " + resFileName);
            isDevNameSet = false;
            Done();
        }

        private delegate void SetTextSafeDelegate(int val);

        private void Progress(int val)
        {
            int progVal = val / total_len * 100;
            if (progressBar1.InvokeRequired) { 
                SetTextSafeDelegate del = new SetTextSafeDelegate(Progress);
                progressBar1.Invoke(del, new object[] { progVal }); 
            } else
            {
                progressBar1.Value = progVal;
            }
        }

        private void Done()
        {
            this.Invoke(new Action(delegate () // this == Form 이다. Form이 아닌 컨트롤의 Invoke를 직접호출해도 무방하다. 
            {
                progressBar1.Value = 100;
                //Invoke를 통해 lbl_Result 컨트롤에 결과값을 업데이트한다. 
                BT_StartDown.Enabled = true;
            }));
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

    }
}
