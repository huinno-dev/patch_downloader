using System;
using System.Diagnostics;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

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

            // Get the driver path.
            int appNameLen = System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.ScopeName.Length;
            string path = System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName;
            string driverPath = path.Substring(0, path.Length - appNameLen) + "driver\\V2.6.0.0\\install.bat";
            Console.WriteLine(driverPath);

            ProcessInfo = new ProcessStartInfo("cmd.exe", "/c " + driverPath);
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.UseShellExecute = false;

            Process = Process.Start(ProcessInfo);
            Process.WaitForExit();

            ExitCode = Process.ExitCode;
            Process.Close();

            Console.WriteLine("ExitCode: " + ExitCode.ToString(), "ExecuteCommand");

            //Thread.Sleep(20000);
        }
    }
}
