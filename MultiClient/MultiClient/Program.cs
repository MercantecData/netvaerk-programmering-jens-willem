using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Collections.Generic;

namespace MultiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            MyServer();
        }
        public static List<TcpClient> clients = new List<TcpClient>();
        public static void MyServer()
        {
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 13376;
            TcpListener listener = new TcpListener(ip, port);
            listener.Start();

            AcceptClients(listener);

            bool isRunning = true;
            while (isRunning)
            {
                //Send msg
                Console.Write("Write message: ");
                string text = Console.ReadLine();
                byte[] buffer = Encoding.UTF8.GetBytes(text);
                foreach(TcpClient client in clients)
                {
                    client.GetStream().Write(buffer, 0, buffer.Length);
                }
            }
        }
        public static async void ReceiveMessages(NetworkStream stream)
        {
            byte[] buffer = new byte[256];
            bool isRunning = true;
            while (isRunning)
            {
                int read = await stream.ReadAsync(buffer, 0, buffer.Length);
                string text = Encoding.UTF8.GetString(buffer, 0, read);
                Console.WriteLine("client writes: " + text);
            }
        }
        public static async void AcceptClients(TcpListener listener)
        {
            bool isRunning = true;
            while (isRunning)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                clients.Add(client);
                NetworkStream stream = client.GetStream();
                ReceiveMessages(stream);
            }
        }
    }
}
