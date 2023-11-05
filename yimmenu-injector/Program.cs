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

string appName = "GTA5";
const string dir = @"c:/yimmenuinjector";
if (!Directory.Exists(dir))
    Directory.CreateDirectory(dir);
using (var client = new WebClient())
{
    client.DownloadFile("https://github.com/YimMenu/YimMenu/releases/download/nightly/YimMenu.dll", @"c:/yimmenuinjector/yimmenu.dll");
    client.DownloadFile("https://hyperion.cat/gtainjector.exe", @"c:/yimmenuinjector/gtainjector.exe");
}

if (userInput.ToUpper() != "N" && userInput.ToUpper() != "Y")
{
    Console.WriteLine("invalid input");
    return;
}

if (userInput.ToUpper() == "Y")
{
    Process p = new Process();
    p.StartInfo.FileName = "C:\\Program Files (x86)\\Steam\\steam.exe";
    p.StartInfo.Arguments = "steam://rungameid/271590";
    p.Start();

    Process[] processes;
    do
    {
        processes = Process.GetProcessesByName(appName);
        if (processes.Length > 0)
        {
            Console.WriteLine("injecting...");
            Thread.Sleep(2000);
            Process.Start("c:/yimmenuinjector/gtainjector.exe");
            break;
        }
        else
        {
            System.Threading.Thread.Sleep(1000);
        }
    } while (true);
}

if (userInput.ToUpper() == "N")
{
    Console.WriteLine("injecting...");
    Thread.Sleep(2000);
    Process.Start("c:/yimmenuinjector/gtainjector.exe");
}
