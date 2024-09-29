using System;
using System.Net.Sockets;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ReceivingApp {
    internal partial class MainWindow : Window {

        TextInputWindow textInputWindow;
        LANManager lanManager;
        Thread receivingThread;
        bool isDisconnectedMessageAdded;

        public MainWindow() {
            InitializeComponent();

            // Создаём и запускаем поток, который принимает сообщения
            receivingThread = new Thread(() => {
                while (true) {
                    try {
                        lanManager = new LANManager();

                        Dispatcher.Invoke(() => {
                            Title = lanManager.GetCurrentAddress().ToString();
                        });

                        try {
                            lanManager.WaitForConnection();
                        } catch (ApplicationException) {
                            Dispatcher.Invoke(() => {
                                MessageBox.Show("Не удалось определить текущий адрес");
                                Close();
                            });
                            return;
                        }

                        isDisconnectedMessageAdded = false;

                        Dispatcher.Invoke(() => {
                            addView(new MessageView("Подключение установлено", Brushes.Green));
                        });

                        while (true) {
                            var tuple = lanManager.Receive();

                            Dispatcher.Invoke(() => {
                                addView(new MessageView(tuple));

                                if (textInputWindow != null) {
                                    textInputWindow.Append(Converter.GetCharacterFromKey(tuple.Item1, tuple.Item2, tuple.Item3));
                                }
                            });
                        }
                    } catch (SocketException) {
                        if (!isDisconnectedMessageAdded) {
                            Dispatcher.Invoke(() => {
                                addView(new MessageView("Подключение разорвано", Brushes.Red));
                            });
                            isDisconnectedMessageAdded = true;
                        }
                        continue;
                    }
                }
            }) { IsBackground = true };
            receivingThread.Start();
        }

        // Метод, который добавляет объект класса MessageView на StackPanel
        void addView(MessageView view) {
            if (sp.Children.Count >= 50) {
                sp.Children.RemoveAt(0);
            }

            sp.Children.Add(view);
            sv.ScrollToBottom();
        }

        // Отслеживаем нажатия клавиш ENTER и SPACE
        protected override void OnPreviewKeyDown(KeyEventArgs e) {
            base.OnPreviewKeyDown(e);

            switch (e.Key) {
                case Key.Enter:
                    if (textInputWindow == null) {
                        textInputWindow = new TextInputWindow();
                        textInputWindow.Owner = this;
                        textInputWindow.Show();

                        textInputWindow.Closing += (o, e1) => {
                            textInputWindow = null;
                        };
                    }
                    return;
                case Key.Space:
                    sp.Children.Clear();
                    return;
            }
        }
    }
}