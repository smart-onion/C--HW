using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Database;
namespace UdpServer
{
    class Client
    {
        public EndPoint EndPoint { get; set; }
        public DateTime StartTime { get; set; } = DateTime.Now;
        public int Requests { get; set; }
        public bool isBlocked = false;
    }


    internal class Program
    {
        static object locker = new object();
        static HashSet<Client> clients = new HashSet<Client>();
        static ILogger logger;
        static int maxClientConnected = 1;

        static ApplicationContext db = new ApplicationContext();

        private static async Task Main(string[] args)
        {
            using ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            logger = loggerFactory.CreateLogger<Program>();

            UdpClient udpClient = new UdpClient();
            udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, 9001));
            logger.LogInformation("Udp server started at port 9001");
            Task.Run(() => HandleClientTimeout());
            await HandleClientsAsync(udpClient);

        }

        static async Task HandleClientsAsync(UdpClient udpClient)
        {
            while (true)
            {
                var result = await udpClient.ReceiveAsync();

                lock (locker)
                {
                    if (clients.FirstOrDefault(c => c.EndPoint.Equals(result.RemoteEndPoint)) == null && clients.Count < maxClientConnected)
                    {
                        logger.LogInformation($"{result.RemoteEndPoint} has been connected");
                        clients.Add(new Client { EndPoint = result.RemoteEndPoint });
                    }
                }

                logger.LogInformation($"Connected clients {clients.Count}");

                var client = clients.FirstOrDefault(c => c.EndPoint.Equals(result.RemoteEndPoint));

                if (client != null && client.Requests <= 10)
                {
                    client.Requests++;
                    client.StartTime = DateTime.Now;

                    HandleClientMessagesAsync(result, udpClient);
                }

            }
        }

        static async void HandleClientMessagesAsync(UdpReceiveResult result, UdpClient udpClient)
        {
            string message = Encoding.UTF8.GetString(result.Buffer);
            logger.LogInformation($"Message from {result.RemoteEndPoint}: {message}");
            if (message == "END")
            {
                logger.LogInformation($"{result.RemoteEndPoint} sent disconnect request.");
                clients.Remove(clients.FirstOrDefault(c => c.EndPoint == result.RemoteEndPoint)!);
                return;
            }

            
            var part = db.spareParts.FirstOrDefault(s => s.Name == message);
            if (part != null)
            {
                var jsonString = JsonSerializer.Serialize(part);
                var data = Encoding.UTF8.GetBytes(jsonString);
                logger.LogInformation($"Sending SparePart to {result.RemoteEndPoint}: {jsonString}");
                await udpClient.SendAsync(data, data.Length, result.RemoteEndPoint);
            }
            else
            {
                logger.LogInformation($"'{message}' not exist!");
                var data = Encoding.UTF8.GetBytes("Not found");
                await udpClient.SendAsync(data, data.Length, result.RemoteEndPoint);
            }
            

        }

        static void HandleClientTimeout()
        {
            while (true)
            {
                lock (locker)
                {
                    foreach (var client in clients)
                    {
                        if (client.Requests > 10 && !client.isBlocked)
                        {
                            client.isBlocked = true;
                            maxClientConnected++;
                            logger.LogWarning($"{client.EndPoint} too many requests exceeds");
                        }

                        if ((DateTime.Now - client.StartTime).TotalMinutes >= 60)
                        {
                            client.isBlocked = false;
                            client.Requests = 0;
                            maxClientConnected--;
                            logger.LogInformation($"{client.EndPoint} has been unblocked.");


                        }

                        if ((DateTime.Now - client.StartTime).TotalMinutes >= 10)
                        {
                            logger.LogInformation($"Client Timeout exceed: {client.EndPoint}");
                            clients.Remove(client);
                        }
                    }
                }
            }
        }
    }
}