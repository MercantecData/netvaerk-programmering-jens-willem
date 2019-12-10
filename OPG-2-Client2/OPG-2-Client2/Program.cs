using System;
using System.Net.Sockets;
using System.Net;
using System.Text;


namespace OPG_2_Client2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1 for client\n2 for server\n3 for server with ACK\n4 for client with response");
            string input = Console.ReadLine();
            if(input == "1")
            {
                Console.WriteLine("Write your message");
                LaunchClient(Console.ReadLine());
            }
            else if(input == "2")
            {
                LaunchServer();
            }
            else if (input == "3")
            {
                LaunchServerACK();
            }
            else if (input == "4")
            {
                Console.WriteLine("Write your message");
                LaunchClientACK(Console.ReadLine());
            }
            else
            {
                Console.WriteLine("1 - 4");
            }
        }
        static void LaunchClient(string text)
        {
            TcpClient client = new TcpClient();
            int port = 13376;
            IPAddress ip = IPAddress.Parse("172.16.116.240");
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            client.Connect(endPoint);
            NetworkStream stream = client.GetStream();
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            stream.Write(buffer, 0, buffer.Length);
            client.Close();
        }
        static void LaunchServerACK()
        {
            int port = 13376;
            IPAddress ipLocal = IPAddress.Any;
            IPEndPoint localEndPoint = new IPEndPoint(ipLocal, port);
            TcpListener listener = new TcpListener(localEndPoint);
            listener.Start();
            Console.WriteLine("Awaiting Clients");
            TcpClient client = listener.AcceptTcpClient();
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[256];
            int numberOfBytesRead = stream.Read(buffer, 0, 256);
            string message = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);
            Console.WriteLine(message);

            string text = "Message Received";
            byte[] buffer2 = Encoding.UTF8.GetBytes(text);
            stream.Write(buffer2, 0, buffer2.Length);
            client.Close();
        }
        static void LaunchServer()
        {
            int port = 13376;
            IPAddress ipLocal = IPAddress.Any;
            IPEndPoint localEndPoint = new IPEndPoint(ipLocal, port);
            TcpListener listener = new TcpListener(localEndPoint);
            listener.Start();
            Console.WriteLine("Awaiting Clients");
            TcpClient client = listener.AcceptTcpClient();
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[256];
            int numberOfBytesRead = stream.Read(buffer, 0, 256);
            string message = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);
            Console.WriteLine(message);
        }
        static void LaunchClientACK(string text)
        {
            TcpClient client = new TcpClient();
            int port = 13376;
            IPAddress ip = IPAddress.Parse("172.16.116.240");
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            client.Connect(endPoint);
            NetworkStream stream = client.GetStream();
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            stream.Write(buffer, 0, buffer.Length);

            IPAddress ipLocal = IPAddress.Any;
            IPEndPoint localEndPoint = new IPEndPoint(ipLocal, port);
            TcpListener listener = new TcpListener(localEndPoint);
            listener.Start();
            Console.WriteLine("Awaiting Clients");
            buffer = new byte[256];
            int numberOfBytesRead = stream.Read(buffer, 0, buffer.Length);
            string message = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);
            Console.WriteLine(message);
        }
    }
}
