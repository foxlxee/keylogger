using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SendingApp {
    // Класс, который отслеживает нажатие клавиш
    internal static class KeyboardTracker {

        // Дескриптор перехватчика
        static IntPtr hHook;
        // Ссылка на процедуру перехватчика
        static WinAPI.HookProc hookProc;

        // Статический конструктор
        static KeyboardTracker() {
            hHook = IntPtr.Zero;
            hookProc = proc;
        }

        // Процедура перехватчика
        static IntPtr proc(int nCode, IntPtr wParam, IntPtr lParam) {
            // Если nCode меньше нуля, не обрабатываем этот вызов
            if (nCode >= 0) {
                // Извлекаем состояние клавиши
                int iwParam = wParam.ToInt32();

                // Извлекаем клавишу
                Keys key = (Keys)Marshal.ReadInt32(lParam);

                // Проверяем состояние
                if ((iwParam == WinAPI.WM_KEYDOWN || iwParam == WinAPI.WM_SYSKEYDOWN)) {
                    // Вызываем событие если на него подписаны
                    KeyDown?.Invoke(key);
                }

                // Проверяем состояние
                else if ((iwParam == WinAPI.WM_KEYUP || iwParam == WinAPI.WM_SYSKEYUP)) {
                    // Вызываем событие если на него подписаны
                    KeyUp?.Invoke(key);
                }
            }

            // Возвращаем системе результат вызова следующего перехватчика в цепочке перехватчиков
            return WinAPI.CallNextHookEx(hHook, nCode, wParam, lParam);
        }

        // Свойство, которое устанавливается в true когда отслеживание запущено
        public static bool IsTrackingStarted { get; private set; }

        // События, на которые можно подписаться
        public static event Action<Keys> KeyDown;
        public static event Action<Keys> KeyUp;

        // Метод, который запускает отслеживание
        public static void StartTracking() {
            if (IsTrackingStarted) return;

            ProcessModule module = Process.GetCurrentProcess().MainModule;
            string moduleName = module.ModuleName;
            module.Dispose();

            hHook = WinAPI.SetWindowsHookEx(WinAPI.WH_KEYBOARD_LL, hookProc, WinAPI.GetModuleHandle(moduleName), 0);

            IsTrackingStarted = true;
        }

        // Метод, который останавливает отслеживание
        public static void StopTracking() {
            if (!IsTrackingStarted) return;

            WinAPI.UnhookWindowsHookEx(hHook);
            IsTrackingStarted = false;
        }

        // Метод, который возвращает текущую раскладку клавиатуры
        public static int GetCurrentKeyboardLayout() {
            IntPtr hwnd = WinAPI.GetForegroundWindow();
            uint pid = WinAPI.GetWindowThreadProcessId(hwnd, IntPtr.Zero);
            int keyboardLayout = WinAPI.GetKeyboardLayout(pid).ToInt32() & 0xFFFF;
            return keyboardLayout;
        }
    }
}