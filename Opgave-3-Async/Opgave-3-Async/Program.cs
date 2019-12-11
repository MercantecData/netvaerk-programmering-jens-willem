using System;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Opgave_3_Async
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener listener = StartServer();
            while (true)
            {
                Server(listener);
                Client();
            }
        }
        static void Client()
        {
            TcpClient client = new TcpClient();
            int port = 13356;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            client.Connect(endPoint);

            NetworkStream stream = client.GetStream();
            ReceiveMessage(stream);

            Console.WriteLine("Type your message here: ");
            string text = Console.ReadLine();
            byte[] buffer = Encoding.UTF8.GetBytes(text);

            stream.Write(buffer, 0, buffer.Length);
        }
        static TcpListener StartServer()
        {
            int port = 13356;
            IPAddress ip = IPAddress.Any;
            IPEndPoint localEndPoint = new IPEndPoint(ip, port);
            TcpListener listener = new TcpListener(localEndPoint);

            listener.Start();
            return listener;
        }
        static async void Server(TcpListener listener) { 
            TcpClient client = await listener.AcceptTcpClientAsync();

            NetworkStream stream = client.GetStream();
            ReceiveMessage(stream);
            
        }
        static async void ReceiveMessage(NetworkStream stream)
        {
            byte[] buffer = new byte[256];
            int numberOfBytesRead = await stream.ReadAsync(buffer, 0, buffer.Length); 
            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);
            Console.WriteLine("Not you: " + receivedMessage);
        }
        //Kan man lave en server der arbejder sammen med flere klienter på samme tid?
        //Svar: Ja
    }
}
