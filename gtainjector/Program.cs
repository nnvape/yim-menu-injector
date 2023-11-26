#include <iostream>
#include <windows.h>
#include <tlhelp32.h>
#include <string>

using namespace std;

int Inject(DWORD pid, const wchar_t* name);

int main()
{
    const wchar_t dll[] = L"c:/yimmenuinjector/yimmenu.dll";
    DWORD pid = 0;

    HANDLE hSnapshot = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
    if (hSnapshot != INVALID_HANDLE_VALUE)
    {
        PROCESSENTRY32 entry;
        entry.dwSize = sizeof(entry);

        if (Process32First(hSnapshot, &entry))
        {
            do
            {
                if (wstring(entry.szExeFile) == L"GTA5.exe")
                {
                    pid = entry.th32ProcessID;
                    break;
                }
            } while (Process32Next(hSnapshot, &entry));
        }

        CloseHandle(hSnapshot);
    }

    if (pid == 0)
    {
        wcout << L"Unable to find the target process" << endl;
        return 0;
    }

    if (Inject(pid, dll))
    {
        wcout << L"DLL has been injected into the process successfully" << endl;
    }
    else
    {
        wcout << L"Couldn't inject DLL into the process" << endl;
    }

    return 0;
}

int Inject(DWORD pid, const wchar_t* name)
{
    HANDLE hProcess, hThread;
    SIZE_T BytesWritten;
    LPVOID mem;

    hProcess = OpenProcess(PROCESS_ALL_ACCESS, FALSE, pid);

    if (!hProcess)
        return 0;

    mem = VirtualAllocEx(hProcess, NULL, wcslen(name) * sizeof(wchar_t), MEM_RESERVE | MEM_COMMIT, PAGE_READWRITE);

    if (mem == NULL)
    {
        CloseHandle(hProcess);
        return 0;
    }

    if (WriteProcessMemory(hProcess, mem, (LPVOID)name, wcslen(name) * sizeof(wchar_t), &BytesWritten))
    {
        hThread = CreateRemoteThread(hProcess, NULL, 0, (LPTHREAD_START_ROUTINE)GetProcAddress(GetModuleHandle(L"KERNEL32.DLL"), "LoadLibraryW"), mem, 0, NULL);

        if (!hThread)
        {
            VirtualFreeEx(hProcess, NULL, wcslen(name) * sizeof(wchar_t), MEM_RESERVE | MEM_COMMIT);
            CloseHandle(hProcess);
            return 0;
        }

        VirtualFreeEx(hProcess, NULL, wcslen(name) * sizeof(wchar_t), MEM_RESERVE | MEM_COMMIT);
        CloseHandle(hThread);
        CloseHandle(hProcess);

        return 1;
    }

    VirtualFreeEx(hProcess, NULL, wcslen(name) * sizeof(wchar_t), MEM_RESERVE | MEM_COMMIT);
    CloseHandle(hProcess);

    return 0;
}
