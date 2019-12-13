using System;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;

namespace Opgave_3_Async
{
    class Program
    {
        static public NetworkStream stream;
        static void Main(string[] args)
        {
            //for at bruge det her program som client fjern: ConnectToClient TcpListener... og Server
            //for server fjern: NetworkStream stream... og Client
            bool isRunning = true;
            TcpListener listener = InitiateServer();
            ConnectToClient(listener);
            NetworkStream stream = InitiateClient();
            Console.ReadKey();
            while (isRunning)
            {
                Server(listener);
                Client(stream);
            }
        }
        static async void Client(NetworkStream stream)
        {
            ReceiveMessage(stream);
            SendMessage(stream);
            await Task.Delay(1000);
            Console.Write("client: ");
        }
        static NetworkStream InitiateClient()
        {
            TcpClient client = new TcpClient();
            int port = 13356;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            client.Connect(endPoint);

            NetworkStream stream = client.GetStream();
            return stream;
        }
        static async void Server(TcpListener listener)
        {
            ReceiveMessage(stream);
            SendMessage(stream);
            await Task.Delay(1000);
            Console.Write("server: ");
        }
        static TcpListener InitiateServer()
        {
            int port = 13356;
            IPAddress ip = IPAddress.Any;
            IPEndPoint localEndPoint = new IPEndPoint(ip, port);
            TcpListener listener = new TcpListener(localEndPoint);

            listener.Start();
            return listener;
        }
        static async void ConnectToClient(TcpListener listener)
        {
            TcpClient client = await listener.AcceptTcpClientAsync();
            stream = client.GetStream();
        }
        static async void ReceiveMessage(NetworkStream stream)
        {
            byte[] buffer = new byte[256];
            int numberOfBytesRead = await stream.ReadAsync(buffer, 0, buffer.Length); 
            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, numberOfBytesRead);
            Console.WriteLine("\n" + "Not you: " + receivedMessage);
        }
        static void SendMessage(NetworkStream stream)
        {
            string text = Console.ReadLine();
            byte[] buffer = Encoding.UTF8.GetBytes(text);

            stream.Write(buffer, 0, buffer.Length);
        }
        //Kan man lave en server der arbejder sammen med flere klienter på samme tid?
        //Svar: Ja
    }
}
