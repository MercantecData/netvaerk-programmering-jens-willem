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

            int o = 1;

            if (o == 1)
            {
                sender();
            }
            else
            {
                reciver();
            }

            

        }
        static void sender()
        {
            // Local sender:
            TcpClient client = new TcpClient();
            int port = 13376;
            IPAddress ip = IPAddress.Parse("172.16.115.87");
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            client.Connect(endPoint);
            NetworkStream stream = client.GetStream();

            string text = "hope this works.";
            byte[] buffer = Encoding.UTF8.GetBytes(text);

            stream.Write(buffer, 0, buffer.Length);

            client.Close();
        }
        static void reciver()
        {
            // Local reciver
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
        }
    }
}
