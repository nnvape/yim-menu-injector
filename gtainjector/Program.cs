using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

class ManualMapInjector
{
    [DllImport("kernel32.dll")]
    public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll")]
    public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

    [DllImport("kernel32.dll")]
    public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

    [DllImport("kernel32.dll")]
    public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, out uint lpThreadId);

    [DllImport("kernel32.dll")]
    public static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("kernel32.dll")]
    public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

    public static void Inject(string targetProcessName, string dllPath)
    {
        Process targetProcess = null;
        Process[] processes = Process.GetProcessesByName(targetProcessName);

        if (processes.Length > 0)
        {
            targetProcess = processes[0];
        }
        else
        {
            Console.WriteLine("Target process not found.");
            return;
        }

        IntPtr hProcess = OpenProcess(0x1F0FFF, false, targetProcess.Id);
        if (hProcess == IntPtr.Zero)
        {
            Console.WriteLine("Failed to open target process.");
            return;
        }

        byte[] dllBytes = System.IO.File.ReadAllBytes(dllPath);
        IntPtr allocAddress = VirtualAllocEx(hProcess, IntPtr.Zero, (uint)dllBytes.Length, 0x1000, 0x40);

        int bytesWritten;
        if (!WriteProcessMemory(hProcess, allocAddress, dllBytes, (uint)dllBytes.Length, out bytesWritten))
        {
            Console.WriteLine("Failed to write DLL into the target process.");
            return;
        }

        IntPtr loadLibraryAddr = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");

        if (loadLibraryAddr == IntPtr.Zero)
        {
            Console.WriteLine("Failed to find the address of LoadLibraryA.");
            return;
        }

        uint threadId;
        IntPtr hThread = CreateRemoteThread(hProcess, IntPtr.Zero, 0, loadLibraryAddr, allocAddress, 0, out threadId);

        if (hThread == IntPtr.Zero)
        {
            Console.WriteLine("Failed to create a remote thread.");
            return;
        }

        Console.WriteLine("DLL injected successfully.");
    }

    public static void Main(string[] args)
    {
        string targetProcessName = "GTA5";
        string dllPath = "c:/yimmenuinjector/yimmenu.dll";

        Inject(targetProcessName, dllPath);
    }
}
