#include <iostream>
#include <thread>
#include <chrono>
#include <cstdlib>
#include <ctime>
#include <string>
#include <windows.h>
#include <tlhelp32.h>
#include <fstream>
#include <Urlmon.h>
#pragma comment(lib, "Urlmon.lib")

using namespace std;

std::string RandomString(const int len)
{
    static const char alphanum[] =
        "0123456789"
        "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        "abcdefghijklmnopqrstuvwxyz";
    std::string tmp_s;
    tmp_s.reserve(len);

    for (int i = 0; i < len; ++i) {
        tmp_s += alphanum[rand() % (sizeof(alphanum) - 1)];
    }

    return tmp_s;
}

void NameChanger()
{
    std::string NAME = (std::string)("yim menu injector | " + RandomString(50));
    SetConsoleTitleA(NAME.c_str());
}
DWORD ChangeName(LPVOID in)
{

    while (true)
    {
        Sleep(1);
        NameChanger();
    }
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

void injectdll() {
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
                if (wcscmp(entry.szExeFile, L"GTA5.exe") == 0)
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
        return;
    }

    if (Inject(pid, dll))
    {

    }
    else
    {

    }

    return;
}


int getConsoleWindowWidth() {
    CONSOLE_SCREEN_BUFFER_INFO csbi;
    int columns;
    GetConsoleScreenBufferInfo(GetStdHandle(STD_OUTPUT_HANDLE), &csbi);
    columns = csbi.srWindow.Right - csbi.srWindow.Left + 1;
    return columns;
}

void printCentered(const std::string& text) {
    int width = getConsoleWindowWidth();
    int padding = (width - text.length()) / 2;
    for (int i = 0; i < padding; ++i) {
        std::cout << " ";
    }
    std::cout << text << std::endl;
}

std::string getCenteredInput(const std::string& prompt) {
    int width = getConsoleWindowWidth();
    int promptLength = prompt.length() + 3;

    int padding = (width - promptLength) / 2;
    for (int i = 0; i < padding; ++i) {
        std::cout << " ";
    }

    std::cout << prompt << std::endl;
    std::cout << "> ";
    std::string input;
    std::cin >> input;
    return input;
}

void launchgtasteam() {
    HANDLE hProcessSnap;
    PROCESSENTRY32W pe32;
    DWORD dwPriorityClass;
    system("start \"\" \"C:\Program Files (x86)\\Steam\\steam.exe\" \"steam://rungameid/271590\" \"-silent\" \"-minimized\"");

    do {
        hProcessSnap = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
        if (hProcessSnap == INVALID_HANDLE_VALUE) {
            return;
        }

        pe32.dwSize = sizeof(PROCESSENTRY32W);

        if (!Process32FirstW(hProcessSnap, &pe32)) {
            CloseHandle(hProcessSnap);
            return;
        }

        do {
            if (wcscmp(pe32.szExeFile, L"GTA5.exe") == 0) {
                std::wcout << L"detected! injecting..." << std::endl;
                Sleep(2000);
                injectdll();
                CloseHandle(hProcessSnap);
                return;
            }
        } while (Process32NextW(hProcessSnap, &pe32));

        CloseHandle(hProcessSnap);
        std::this_thread::sleep_for(std::chrono::milliseconds(1000));
    } while (true);
}

void launchgtaepic() {
    HANDLE hProcessSnap;
    PROCESSENTRY32W pe32;
    DWORD dwPriorityClass;

    system("\"C:\\Program Files\\Epic Games\\GTA V\\GTA5.exe\"");

    do {
        hProcessSnap = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
        if (hProcessSnap == INVALID_HANDLE_VALUE) {
            return;
        }

        pe32.dwSize = sizeof(PROCESSENTRY32W);

        if (!Process32FirstW(hProcessSnap, &pe32)) {
            CloseHandle(hProcessSnap);
            return;
        }

        do {
            if (wcscmp(pe32.szExeFile, L"GTA5.exe") == 0) {
                std::wcout << L"detected! injecting..." << std::endl;
                Sleep(2000);
                injectdll();
                CloseHandle(hProcessSnap);
                return;
            }
        } while (Process32NextW(hProcessSnap, &pe32));

        CloseHandle(hProcessSnap);
        std::this_thread::sleep_for(std::chrono::milliseconds(1000));
    } while (true);
}

void detectinject() {
    HANDLE hProcessSnap;
    PROCESSENTRY32W pe32;
    DWORD dwPriorityClass;

    do {
        hProcessSnap = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
        if (hProcessSnap == INVALID_HANDLE_VALUE) {
            return;
        }

        pe32.dwSize = sizeof(PROCESSENTRY32W);

        if (!Process32FirstW(hProcessSnap, &pe32)) {
            CloseHandle(hProcessSnap);
            return;
        }

        do {
            if (wcscmp(pe32.szExeFile, L"GTA5.exe") == 0) {
                std::wcout << L"detected! injecting..." << std::endl;
                Sleep(2000);
                injectdll();
                CloseHandle(hProcessSnap);
                return;
            }
        } while (Process32NextW(hProcessSnap, &pe32));

        CloseHandle(hProcessSnap);
        std::this_thread::sleep_for(std::chrono::milliseconds(1000));
    } while (true);
}

void sleepFunction(int milliseconds) {
    std::this_thread::sleep_for(std::chrono::milliseconds(milliseconds));
}

int main() {
    system("mkdir \"c:/yimmenuinjector\"");
    system("cls");
    URLDownloadToFile(NULL, L"https://github.com/YimMenu/YimMenu/releases/download/nightly/YimMenu.dll", L"c:/yimmenuinjector/yimmenu.dll", 0, NULL);
    system("cls");

    CreateThread(NULL, NULL, ChangeName, NULL, NULL, NULL);
    bool running = true;

    while (true) {
        printCentered("          _              _         _           __            ");
        printCentered("   __  __(_)___ ___     (_)___    (_)__  _____/ /_____  _____");
        printCentered("  / / / / / __ `__ \\   / / __ \\  / / _ \\/ ___/ __/ __ \\/ ___/");
        printCentered(" / /_/ / / / / / / /  / / / / / / /  __/ /__/ /_/ /_/ / /    ");
        printCentered(" \\__, /_/_/ /_/ /_/  /_/_/ /_/_/ /\\___/\\___/\\__/_\\____/_/     ");
        printCentered("/____/                      /___/                             ");
        std::cout << std::endl;
        printCentered("1. inject");
        printCentered("2. launch gta v steam");
        printCentered("3. launch gta v epic games");
        printCentered("4. auto detect inject");
        printCentered("5. exit");
        std::string input;
        std::cout << "> ";
        std::getline(std::cin, input);
        if (input == "1") {
            injectdll();
            system("cls");
            continue;
            break;
        }
        else if (input == "2") {
            launchgtasteam();
            system("cls");
            continue;
            break;
        }
        else if (input == "3") {
            launchgtaepic();
            system("cls");
            continue;
            break;
        }
        else if (input == "4") {
            detectinject();
            system("cls");
            continue;
            break;
            bool running = true;
        }
        else if (input == "5") {
            std::exit(0);
            break;
        }
        else {
            std::cout << "invalid choice\n";
            system("cls");
            continue;
        }
        bool running = true;
    }
    return 0;
}
