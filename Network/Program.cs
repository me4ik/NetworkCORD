using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;

namespace Network
{
    internal class Program
    {
        public static void ConnectToOtherHost(TcpClient client)
        {
            while (client.Connected == false)
            {
                Console.WriteLine("Введите адресс, а затем порт для подключения");

                IPAddress IPofServer = IPAddress.Parse(Console.ReadLine());
                int port = Convert.ToInt32(Console.ReadLine());

                try
                {
                    client.Connect(IPofServer, port);
                    Console.WriteLine("Успешное подключение! {0}", client.Connected);
                }

                catch (Exception ex) { Console.WriteLine("Адресс или порт не верный, попробуйте снова."); }
            }
        }

        public static async Task RegisterHostAsync(IPAddress address) //Создание хоста
        {
            //TcpListener server = new TcpListener(IPAddress.Parse(GetLocalIPAddress()), 0);
            TcpListener server = new TcpListener(IPAddress.Parse("25.50.85.46"), 0);
            server.Start();

            try // Инициализвация сервера
            {
                //await client.ConnectAsync((IPEndPoint)server.Server.LocalEndPoint);
                Console.WriteLine("Сервер создан.");
                Console.WriteLine("Адресс сервера: {0}", (IPEndPoint)server.Server.LocalEndPoint);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Не удалось создать сервер");
            }

            while (true)
            {
                // получаем подключение в виде TcpClient
                using var tcpClient = await server.AcceptTcpClientAsync();
                Console.WriteLine($"Входящее подключение: {tcpClient.Client.RemoteEndPoint}");
            }
        }

        public static string GetCurrentTime()
        {
            DateTime currentTime = DateTime.Now;

            int hours = currentTime.Hour;
            int minutes = currentTime.Minute;

            return "[" + hours + ":" + minutes + "]";
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        static async Task Main(string[] args)
        {
            string CordLogo = " ░▒▓██████▓▒░        ░▒▓██████▓▒░       ░▒▓███████▓▒░       ░▒▓███████▓▒░         \r\n░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░        \r\n░▒▓█▓▒░             ░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░        \r\n░▒▓█▓▒░             ░▒▓█▓▒░░▒▓█▓▒░      ░▒▓███████▓▒░       ░▒▓█▓▒░░▒▓█▓▒░        \r\n░▒▓█▓▒░             ░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░      ░▒▓█▓▒░░▒▓█▓▒░        \r\n░▒▓█▓▒░░▒▓█▓▒░▒▓██▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓██▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓██▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓██▓▒░ \r\n ░▒▓██████▓▒░░▒▓██▓▒░░▒▓██████▓▒░░▒▓██▓▒░▒▓█▓▒░░▒▓█▓▒░▒▓██▓▒░▒▓███████▓▒░░▒▓██▓▒░                                                                           ";

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(CordLogo);
            Console.WriteLine("Computer Ordinary Reply Display 0.01v \r\n");

            Console.WriteLine("Создать хост? 1 - да, 0 - нет");

            if (Console.ReadLine() == "1") // Создание сервера и ожидание входящих подключений
            {
                Console.WriteLine("Введите ваш открытый IP адрес (Hamachi, Radmin):");
                IPAddress IPofServer = IPAddress.Parse(Console.ReadLine());

                await RegisterHostAsync(IPofServer);
            }

            else // Создание клиента и подключение к серверу
            {
                Console.WriteLine("Укажите имя пользователя:");
                string userName = Console.ReadLine();

                TcpClient thisUser = new TcpClient();

                ConnectToOtherHost(thisUser);
            }


        }
    }
}