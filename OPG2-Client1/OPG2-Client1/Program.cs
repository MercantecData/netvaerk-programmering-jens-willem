using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace OPG2_Client1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("type one to send mesagge and type 2 to recive.");
            var i = Console.ReadLine().ToString();
            if (i == "1")
            {
                Console.WriteLine("write msg");
                string msg = Console.ReadLine().ToString();
                newsend(msg);
            }
            else if (i == "2")
            {
                rtmsg();
            }
        }
        static void newsend(string msg)
        {
            //send
            TcpClient client = new TcpClient();
            int port = 13376;
            IPAddress ip = IPAddress.Parse("172.16.115.87");
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            client.Connect(endPoint);
            NetworkStream stream = client.GetStream();

           
            byte[] buffer = Encoding.UTF8.GetBytes(msg);

            stream.Write(buffer, 0, buffer.Length);

            //recive
            ip = IPAddress.Any;
            IPEndPoint localEndpoint = new IPEndPoint(ip, port);

            TcpListener listener = new TcpListener(localEndpoint);
            listener.Start();
            Console.WriteLine("redy");

            buffer = new byte[256];
            int numberOfBytesRead = stream.Read(buffer, 0, buffer.Length);
            msg = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);
            Console.WriteLine(msg);
        }
        static void rtmsg()
        {
            int port = 13376;
            IPAddress ip = IPAddress.Any;
            IPEndPoint localEndpoint = new IPEndPoint(ip, port);

            TcpListener listener = new TcpListener(localEndpoint);
            listener.Start();
            Console.WriteLine("redy");
            TcpClient client = listener.AcceptTcpClient();

            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[256];
            int numberOfBytesRead = stream.Read(buffer, 0, buffer.Length);
            string msg = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);
            Console.WriteLine(msg);

            string send = "got it.";

            buffer = Encoding.UTF8.GetBytes(send);

            stream.Write(buffer, 0, buffer.Length);
        }
    }
}
