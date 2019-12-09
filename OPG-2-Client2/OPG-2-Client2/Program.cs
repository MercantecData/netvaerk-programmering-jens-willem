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
            Console.WriteLine("1 for client\n2 for server");
            string input = Console.ReadLine();
            if(input == "1")
            {
                LaunchClient();
            }
            else if(input == "2")
            {
                LaunchServer();
            }
            else
            {
                Console.WriteLine("1 or 2");
            }
        }
        static void LaunchClient()
        {
            TcpClient client = new TcpClient();
            int port = 13376;
            IPAddress ip = IPAddress.Parse("172.16.116.240");
            IPEndPoint endPoint = new IPEndPoint(ip, port);
            client.Connect(endPoint);
            NetworkStream stream = client.GetStream();
            string text = "Hello Jens!";
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            stream.Write(buffer, 0, buffer.Length);
            client.Close();
        }
        static void LaunchServer()
        {
            int port = 13376;
            IPAddress ip = IPAddress.Any;
            IPEndPoint localEndPoint = new IPEndPoint(ip, port);
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
    }
}
