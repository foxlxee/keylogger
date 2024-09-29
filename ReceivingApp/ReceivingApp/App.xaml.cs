using System.Diagnostics;
using System.Threading;
using System.Windows;

namespace ReceivingApp {
    internal partial class App : Application {
        
        Mutex mutex;

        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);

            // Предотвращаем множественный запуск процесса
            string name = "ef724af6-b26f-4840-906b-28b1be56ddc0";
            try {
                mutex = Mutex.OpenExisting(name);
                mutex.Close();
                Process.GetCurrentProcess().Kill();
            } catch {
                mutex = new Mutex(true, name);
            }
        }

        protected override void OnExit(ExitEventArgs e) {
            base.OnExit(e);

            mutex.Close();
        }
    }
}