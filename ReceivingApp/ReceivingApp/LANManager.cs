using System;
using System.Net;
using System.Net.Sockets;

namespace ReceivingApp {
    // Класс, который получает сообщения
    internal class LANManager {

        const int PORT = 4031;

        byte[] buffer;
        Socket client;
        IPEndPoint ipEndPoint;

        // Конструктор, который получает локальный адрес
        public LANManager() {
            buffer = new byte[17];
            
            var host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (IPAddress address in host.AddressList) {
                if (address.AddressFamily == AddressFamily.InterNetwork) {
                    ipEndPoint = new IPEndPoint(address, PORT);
                    break;
                }
            }
        }

        // Метод, который возвращает текущий локальный адрес
        public IPAddress GetCurrentAddress() {
            return ipEndPoint.Address;
        }

        // Метод, который запускает ожидание подключения
        public void WaitForConnection() {
            if (ipEndPoint == null) throw new ApplicationException();

            Socket listeningSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listeningSocket.Bind(ipEndPoint);
            listeningSocket.Listen(1);

            client = listeningSocket.Accept();

            listeningSocket.Close();
        }

        // Метод, который конвертирует полученные данные в кортеж
        public Tuple<Keys, int, bool, DateTime> Receive() {
            if (client == null) throw new Exception("Client is not connected");

            if (client.Receive(buffer) == 0) throw new SocketException();

            return new Tuple<Keys, int, bool, DateTime>(
                (Keys)BitConverter.ToInt32(buffer, 0),
                BitConverter.ToInt32(buffer, 4),
                buffer[8] == 1,
                DateTime.FromBinary(BitConverter.ToInt64(buffer, 9)));
        }
    }
}