using System;
using System.Diagnostics;
using System.IO.Compression;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace yimmenu_injector_headless
{
    static class Program
    {
        private static bool taskExecuted = false;
        private static bool exitRequested = false;
        private static readonly object exitLock = new object();

        [STAThread]
        static void Main()
        {
            ThreadPool.QueueUserWorkItem((state) =>
            {
                string userInput = Console.ReadLine();
                string directoryPath = "C:\\yimmenuinjector\\";
                string extractPath = @"c:/yimmenuinjector";
                String ZipPath = @"c:/yimmenuinjector/binary.zip";
                DirectoryInfo directory = new DirectoryInfo(directoryPath);
                const string dir = @"c:/yimmenuinjector";

                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                using (var client = new WebClient())
                {
                    client.DownloadFile("https://nightly.link/YimMenu/YimMenu/workflows/ci/master/binary.zip", @"c:/yimmenuinjector/binary.zip");
                    ZipFile.ExtractToDirectory(ZipPath, extractPath);
                    File.Delete("c:/yimmenuinjector/binary.zip");
                    File.Delete("c:/yimmenuinjector/yimmenu.dll");

                    foreach (var file in directory.GetFiles("*.dll"))
                    {
                        string newName = Path.Combine(file.DirectoryName, "yimmenu" + file.Extension);
                        file.MoveTo(newName);
                    }

                    client.DownloadFile("https://github.com/nnvape/yim-menu-injector/releases/download/download/gtainjector.exe", @"c:/yimmenuinjector/gtainjector.exe");
                }

                taskExecuted = true;

                bool gta5Running = false;
                do
                {
                    Process[] processes = Process.GetProcessesByName("GTA5");
                    if (processes.Length > 0)
                    {
                        if (!gta5Running)
                        {
                            Thread.Sleep(2000);
                            Process.Start("c:/yimmenuinjector/gtainjector.exe");
                            gta5Running = true;
                            lock (exitLock)
                            {
                                Application.Exit();
                            }
                        }
                    }
                    else
                    {
                        gta5Running = false;
                        Thread.Sleep(1000);
                    }
                } while (true);
            });


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var appThread = new Thread(() =>
            {
                startgta startForm = new startgta();
                startForm.FormClosed += (s, e) =>
                {
                    lock (exitLock)
                    {
                        exitRequested = true;
                        if (taskExecuted)
                        {
                            Application.Exit();
                        }
                    }
                };
                Application.Run(startForm);
            });

            appThread.SetApartmentState(ApartmentState.STA);
            appThread.Start();

            while (!taskExecuted)
            {
                Thread.Sleep(100);
            }
        }
    }
}
