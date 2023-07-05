using System.ComponentModel;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using Server.Lab13;

namespace Server
{
    static class Write
    {
        public static void Red(string str)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    internal class Program
    {
        static int port = 8005;
        static void Main(string[] args)
        {
            IPEndPoint iPEndPoint = new(IPAddress.Parse("127.0.0.1"), port);
            Socket listenSocket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream fileStream = new(@"C:\Study\C#\Lab13\ServerSerialized.bin", FileMode.Create))
            {
                formatter.Serialize(fileStream, new Human());
            }
            try
            {
                listenSocket.Bind(iPEndPoint);
                listenSocket.Listen(10);
                while (true)
                {
                    Socket handler = listenSocket.Accept();
                    StringBuilder builder = new();
                    int bytes = 0;
                    byte[] data = new byte[256];
                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);
                    Console.WriteLine(DateTime.Now.ToShortTimeString() + ": " + builder.ToString());
                    string message = "Ваше сообщение доставлено";
                    data = Encoding.Unicode.GetBytes(message);
                    handler.SendFile(@"C:\Study\C#\Lab13\Serialized.bin");
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception ex)
            {
                Write.Red(ex.Message);
            }
        }
    }
}