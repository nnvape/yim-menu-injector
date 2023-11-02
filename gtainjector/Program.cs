using System.Diagnostics;
using System.Runtime.InteropServices;

public class injector
{
    [DllImport("kernel32.dll")]
    public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

    [DllImport("kernel32.dll")]
    public static extern IntPtr GetModuleHandle(string lpModuleName);

    [DllImport("kernel32.dll")]
    public static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

    [DllImport("kernel32.dll")]
    public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, int dwSize, int flAllocationType, int flProtect);

    [DllImport("kernel32.dll")]
    public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, string lpBuffer, int nSize, out int lpNumberOfBytesWritten);

    [DllImport("kernel32.dll")]
    public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttribute, int dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, int dwCreationFlags, IntPtr lpThreadId);

    public static void InjectDll()
    {
        string processName = "GTA5";
        string dllPath = "C:\\yimmenuinjector\\yimmenu.dll";

        Process[] processes = Process.GetProcessesByName(processName);

        if (processes.Length == 0)
        {
            Console.WriteLine($"No process named {processName} found.");
            return;
        }

        int processId = processes[0].Id;

        IntPtr hProcess = OpenProcess(0x1F0FFF, false, processId);
        if (hProcess == IntPtr.Zero)
        {
            Console.WriteLine("Failed to open process.");
            return;
        }

        IntPtr dllPathAddr = VirtualAllocEx(hProcess, IntPtr.Zero, dllPath.Length, 0x1000, 0x40);
        if (dllPathAddr == IntPtr.Zero)
        {
            Console.WriteLine("Failed to allocate memory in the remote process.");
            return;
        }

        int bytesWritten;
        if (!WriteProcessMemory(hProcess, dllPathAddr, dllPath, dllPath.Length, out bytesWritten))
        {
            Console.WriteLine("Failed to write the DLL path to the remote process.");
            return;
        }

        IntPtr loadLibraryAddr = GetLoadLibraryAddress();
        IntPtr hThread = CreateRemoteThread(hProcess, IntPtr.Zero, 0, loadLibraryAddr, dllPathAddr, 0, IntPtr.Zero);
        if (hThread == IntPtr.Zero)
        {
            Console.WriteLine("Failed to create a remote thread.");
            return;
        }

        Console.WriteLine("DLL injection successful!");
    }

    private static IntPtr GetLoadLibraryAddress()
    {
        IntPtr kernel32 = GetModuleHandle("kernel32.dll");
        if (kernel32 == IntPtr.Zero)
        {
            return IntPtr.Zero;
        }

        return GetProcAddress(kernel32, "LoadLibraryA");
    }

    public static void Main()
    {
        InjectDll();
    }
}


