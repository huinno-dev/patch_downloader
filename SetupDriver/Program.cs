using System;
using System.Diagnostics;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;

namespace SetupDriver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Check and Install Driver ...");

            int ExitCode;
            ProcessStartInfo ProcessInfo;
            Process Process;

            string driverPath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\driver\\V2.6.0.0\\install.bat";
            Console.WriteLine(driverPath);

            ProcessInfo = new ProcessStartInfo("cmd.exe", "/c " + driverPath);
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.UseShellExecute = false;

            Process = Process.Start(ProcessInfo);
            Process.WaitForExit();

            ExitCode = Process.ExitCode;
            Process.Close();

            Console.WriteLine("ExitCode: " + ExitCode.ToString(), "ExecuteCommand");

            //Thread.Sleep(50000);
        }
    }
}
