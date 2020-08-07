using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO.Ports;       // used to access the SerialPort Class
namespace Huinno_Downloader
{
    public class cSerialPort
    {
        public static bool isConnected = false;

        public static SerialPort gComPort;

        public static string[] GetSerialComPortNameList()
        {
            string[] nameArray = SerialPort.GetPortNames();
            nameArray = DupCheck<string>(nameArray);

            return nameArray;
        }
        public static T[] DupCheck<T>(T[] dupArray)
        {
            List<T> result = new List<T>();

            for (int i = 0; i < dupArray.Length; i++)
            {
                if (result.Contains(dupArray[i])) continue;
                result.Add(dupArray[i]);
            }
            return result.ToArray();
        }

        public static string Open(string cport_name, int cport_baud)
        {
            gComPort = new SerialPort(cport_name, cport_baud);
            gComPort.ReadTimeout = 1000; //Setting ReadTimeout =3500 ms or 3.5 seconds

            //int size = gComPort.ReadBufferSize;
            gComPort.ReadBufferSize = 1024 * 4;
            int size = gComPort.ReadBufferSize;

            #region
            try
            {
                gComPort.Open();
            }
            catch (UnauthorizedAccessException SerialException) //exception that is thrown when the operating system denies access 
            {
                gComPort.Close();
                return SerialException.ToString();
                //MessageBox.Show(SerialException.ToString());
                //TextBox_System_Log.Text = Port_Name + Environment.NewLine + Baud_Rate;
                //TextBox_System_Log.Text = TextBox_System_Log.Text + Environment.NewLine + SerialException.ToString();
                //COMport.Close();                                  // Close the Port
            }
            catch (System.IO.IOException SerialException)     // An attempt to set the state of the underlying port failed
            {
                gComPort.Close();
                return SerialException.ToString();
                //MessageBox.Show(SerialException.ToString());
                //TextBox_System_Log.Text = Port_Name + Environment.NewLine + Baud_Rate;
                //TextBox_System_Log.Text = TextBox_System_Log.Text + Environment.NewLine + SerialException.ToString();
                //COMport.Close();                                  // Close the Port
            }
            catch (InvalidOperationException SerialException) // The specified port on the current instance of the SerialPort is already open
            {
                gComPort.Close();
                return SerialException.ToString();
                //MessageBox.Show(SerialException.ToString());
                //TextBox_System_Log.Text = Port_Name + Environment.NewLine + Baud_Rate;
                //TextBox_System_Log.Text = TextBox_System_Log.Text + Environment.NewLine + SerialException.ToString();
                //COMport.Close();                                  // Close the Port
            }
            catch //Any other ERROR
            {
                gComPort.Close();
                return "Failed to open com port : UnKnown";
                //MessageBox.Show("ERROR in Opening Serial PORT -- UnKnown ERROR");
                //COMport.Close();                                  // Close the Port
            }
            #endregion

            gComPort.DiscardInBuffer();
            gComPort.DiscardOutBuffer();

            isConnected = true;
            return "OK";
        }

        public static void Clear()
        {
            gComPort.DiscardInBuffer();
            gComPort.DiscardOutBuffer();
        }

        public static void Close()
        {
            gComPort.Close();
            isConnected = false;
            //Array.Clear(adcDrawArray, 0, adcDrawArray.Length);
        }

        public static void Write(byte[] msg, int length)
        {
            gComPort.Write(msg, 0, length);
        }

        public static UInt32 gSerialReadSize = 1024;
        public static string ReadExisting()
        {
            return gComPort.ReadExisting();
        }
        public static string ReadLine()
        {
            return gComPort.ReadLine();
        }

        public static int ReadCnt(byte[] rBuf)
        {
            int rCnt = 0;
            while (true)
            {
                try
                {
                    rCnt += gComPort.Read(rBuf, 0, 1024);
                    if (rCnt == 1024)
                        break;
                }
                catch (TimeoutException ex)
                {
                    gComPort.Close();
                    return -1;
                }
                catch (System.UnauthorizedAccessException ex)
                {
                    gComPort.Close();
                    return -2;
                }
                catch (System.InvalidOperationException)
                {
                    gComPort.Close();
                    return -3;
                }
                catch (System.IO.IOException ex)
                {
                    gComPort.Close();
                    return -4;
                }
            }
            return rCnt;
        }
        public static int Read(byte[] rBuf)
        {
            int readCnt = 0;

            //rBuffer_tmp = new byte[rBuffer.Length];
            while (true)
            {
                try
                {
                    // Array.Copy(rBuffer, 0, rBuffer_tmp, 0, rBuffer_tmp.Length);
                    //readCnt += COMport.Read(rBuffer, offset, (int)PACKETSIZE - offset);

                    readCnt += gComPort.Read(rBuf, readCnt, 1024 * 4 - readCnt);
                    if (readCnt == 1024 * 4)
                        break;

                    //if (readCnt > 3 && rBuf[0] == 0xE8 && NandRead == false)
                    //{
                    //    if (rBuffer[2] + 3 == readCnt)
                    //    {
                    //        readCnt = 0;
                    //        break;
                    //    }
                    //}
                }
                catch (TimeoutException ex)
                {


                    //return -1;
                }
                catch (System.UnauthorizedAccessException ex)
                {
                    //gComPort.Close();
                    return -2;
                }
                catch (System.InvalidOperationException)
                {
                    //gComPort.Close();
                    return -3;
                }
                catch (System.IO.IOException ex)
                {
                    //gComPort.Close();
                    return -4;
                }
            }

            return readCnt;
        }
    }
}
