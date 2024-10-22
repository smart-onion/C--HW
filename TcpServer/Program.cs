using LocalLibrary;
using System.Net.Sockets;
using System.Net;
using System.Text.Json;
using System.Text;
internal class Program
{
    static object obj = new object();
    static Queue<Order?> orders = new Queue<Order?>();
    private static async Task Main(string[] args)
    {
        TcpListener tcpListener = new TcpListener(IPAddress.Any, 8080);
        tcpListener.Start();

        while (true)
        {
            try
            {
                var client = await tcpListener.AcceptTcpClientAsync();
                Console.WriteLine($"New connection {client.Client.RemoteEndPoint} established.");
                Task.Run(() => HandleClientAsync(client));
                Task.Run(() => HandleOrdersAsync(client));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }

    static async void HandleOrdersAsync(TcpClient tcpClient)
    {
        var stream = tcpClient.GetStream();
        while (tcpClient.Connected)
        {
            
            for (int i = 0; i < orders.Count; i++) 
            {
                Order? result;
                lock (obj)
                {
                    orders.TryDequeue(out result);
                }

                await Task.Delay(2000); // simulating work
                if (result != null)
                {
                    result.isReady = true;
                    stream.WriteByte(5);
                    var jsonString = JsonSerializer.Serialize(result);
                    jsonString += '\0';
                    var data = Encoding.UTF8.GetBytes(jsonString);

                    await stream.WriteAsync(data);
                    SendMessageAsync(stream, $"Ored: {result!.Id} ready!");
                }
            }
            
        }
    }

    static async void HandleClientAsync(TcpClient tcpClient)
    {
        var stream = tcpClient.GetStream();
        while (tcpClient.Connected)
        {
            try
            {
                int action = stream.ReadByte();

                switch (action)
                {
                    case 1: // new order
                        var newOrder = await ReceiveOrderAsync(stream);
                        lock (obj) orders.Enqueue(newOrder);
                        Console.WriteLine(newOrder + " added");
                        SendMessageAsync(stream, $"Order {newOrder.Id} in queue");
                        break;
                    case 2: // edit order
                        newOrder = await ReceiveOrderAsync(stream);
                        var order = orders.FirstOrDefault(o => o.Id == newOrder.Id);
                        order.Name = newOrder.Name;
                        Console.WriteLine(newOrder + " edit");

                        SendMessageAsync(stream, $"Order {newOrder.Id} has been edit.");

                        break;
                    case 3: //remove order
                        newOrder = await ReceiveOrderAsync(stream);
                        order = orders.FirstOrDefault(o => o.Id == newOrder.Id);
                        order = null;
                        Console.WriteLine(newOrder + " removed");

                        SendMessageAsync(stream, $"Order {newOrder.Id} has been removed.");

                        break;
                    default:
                        SendMessageAsync(stream, $"Bad request!");
                        break;
                }
            }
            catch (Exception ex)
            {
                stream.Close();
                tcpClient.Close();
                Console.WriteLine(ex.Message);
            }
        }
    }

    static async Task<Order> ReceiveOrderAsync(NetworkStream stream)
    {
        var data = new List<byte>();
        int currentByte;

        do
        {
            currentByte = stream.ReadByte();
            if (currentByte == '\0') break;
            data.Add((byte)currentByte);
        } while (true);
        Order order = JsonSerializer.Deserialize<Order>(data.ToArray());

        return order;
    }

    static async void SendMessageAsync(NetworkStream stream, string message)
    {
        await stream.WriteAsync(Encoding.UTF8.GetBytes(message + '\0'));
    }
}