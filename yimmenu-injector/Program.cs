using System;
using System.Diagnostics;
using System.IO.Compression;
using System.Net;
using System.IO;
using Figgle;
using System.Runtime.InteropServices;
using System.Text;

Console.WriteLine(FiggleFonts.Standard.Render("yim menu injector"));
Console.WriteLine("injector made by vape");
Console.WriteLine("cheat made by yimura");
Console.WriteLine("should GTA V steam edition launch? (y/n)");

string userInput = Console.ReadLine();

if (userInput.ToUpper() == "Y")
{
    Process p = new Process();
    p.StartInfo.FileName = "C:\\Program Files (x86)\\Steam\\steam.exe";
    p.StartInfo.Arguments = "steam://rungameid/271590";
    p.Start();
    string appName = "GTA5";
    Process[] processes;
    do
    {
        processes = Process.GetProcessesByName(appName);
        if (processes.Length > 0)
        {
            Console.WriteLine("downloading assets...");
            string dir = @"c:/yimmenuinjector";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            WebClient webClient = new WebClient();
            webClient.DownloadFile("https://github.com/YimMenu/YimMenu/releases/download/nightly/YimMenu.dll", @"c:/yimmenuinjector/yimmenu.dll");
            webClient.DownloadFile("https://hyperion.cat/gtainjector.exe", @"c:/yimmenuinjector/gtainjector.exe");
            Thread.Sleep(2000);
            Console.WriteLine("injecting...");
            Thread.Sleep(2000);
            Process.Start("c:/yimmenuinjector/gtainjector.exe");
            break;
        }
        else
        {
            Thread.Sleep(1000);
        }
    } while (true);
}

    if (userInput.ToUpper() == "N")
    {
        Console.WriteLine("downloading assets...");
        string dir = @"c:/yimmenuinjector";
        if (!Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }
        WebClient webClient = new WebClient();
        webClient.DownloadFile("https://github.com/YimMenu/YimMenu/releases/download/nightly/YimMenu.dll", @"c:/yimmenuinjector/yimmenu.dll");
        webClient.DownloadFile("https://hyperion.cat/gtainjector.exe", @"c:/yimmenuinjector/gtainjector.exe");
        Thread.Sleep(2000);
        Console.WriteLine("injecting...");
        Thread.Sleep(2000);
        Process.Start("c:/yimmenuinjector/gtainjector.exe");
    }

