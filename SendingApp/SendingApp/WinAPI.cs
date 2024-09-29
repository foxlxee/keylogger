using System;
using System.Runtime.InteropServices;

namespace SendingApp {
    // Класс, который содержит методы, которые вызывают неуправляемый код из системных библиотек
    internal static class WinAPI {
        // Определяем константы, которые используются в процедуре перехватчика
        public const int WM_KEYDOWN = 0x100;
        public const int WM_SYSKEYDOWN = 0x104;
        public const int WM_KEYUP = 0x101;
        public const int WM_SYSKEYUP = 0x105;
        public const int WH_KEYBOARD_LL = 13;

        // Определяем сигнатуру метода процедуры перехватчика
        public delegate IntPtr HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        // Функция, которая устанавливает определяемую приложением процедуру перехватчика в цепочку перехватчиков
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        // Функция, которая удаляет процедуру перехватчика, установленную в цепочке перехватчиков
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        // Функция, которая передает сведения о перехватчике в следующую процедуру перехватчика в текущей цепочке перехватчиков
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        // Функция, которая возвращает дескриптор указанного модуля
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        // Функция, которая возвращает дескриптор активного окна
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetForegroundWindow();

        // Функция, которая возвращает идентификационный номер потока, создавшего окно
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hwnd, IntPtr processId);

        // Функция, которая возвращает текущую раскладку клавиатуры
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetKeyboardLayout(uint thread);
    }
}