using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Huinno_Downloader
{
    static class Program
    {
        private enum ShowWindowEnum
        {
            Hide = 0,
            ShowNormal = 1, 
            ShowMinimized = 2, 
            ShowMaximized = 3,
            Maximize = 3, 
            ShowNormalNoActivate = 4,
            Show = 5,
            Minimize = 6, 
            ShowMinNoActivate = 7, 
            ShowNoActivate = 8,
            Restore = 9, 
            ShowDefault = 10, 
            ForceMinimized = 11
        };

        private enum WindowPos
        {
            HWND_NOTOPMOST = -2,
            HWND_TOPMOST = -1,
            HWND_TOP = 0,
            HWND_BOTTOM = 1
        };


        [DllImport("user32.dll")]
        internal static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        static extern void SwitchToThisWindow(IntPtr hWnd, bool turnOn);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, int uFlags);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_SHOWMAXIMIZED = 3;


        static bool isProcess;
        static System.Threading.Mutex mutex;
        static string appName = "Huinno_Dataloader";
        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            mutex = new Mutex(true, appName, out isProcess);

            //GC.KeepAlive(mutex);

            if (!isProcess)
            {
                // get the process
                Process[] bProcess = Process.GetProcessesByName(appName);

                //MessageBox.Show(String.Format("version : {0}", bProcess.Length));

                for (int i = 0; i < bProcess.Length; i++)
                {
                    // check if the process is running
                    if (bProcess[i] != null)
                    {
                        //ShowWindow(bProcess[i].MainWindowHandle, (int) ShowWindowEnum.Restore);
                        ShowWindowAsync(bProcess[i].MainWindowHandle, SW_SHOWNORMAL);
                        SetForegroundWindow(bProcess[i].MainWindowHandle);
                        //SetWindowPos(bProcess[i].MainWindowHandle, HWND_TOPMOST, 0, 0, 0, 0, 0x0040 | 0x0001 | 0x0002);  //SWP_SHOWWINDOW | SWP_NOSIZE | SWP_NOMOVE
                        //SwitchToThisWindow(bProcess[i].MainWindowHandle, true);
                    }
                }

                mutex.ReleaseMutex();

                return;
            }

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                main_window w = new main_window();

                if (w.m_loginPass == false)
                {
                    w.Close();
                    mutex.ReleaseMutex();
                    return;

                }
                if (w.m_strCfg_uploadurl == "")
                {
                    w.Close();
                    mutex.ReleaseMutex();
                    return;
                }

                Application.Run(w);
            }
            finally { mutex.ReleaseMutex(); }
            
        }
    }
}
