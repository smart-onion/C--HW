using System.Net.Sockets;
using System.Net;
using System.Text;
using LocalLibrary;
using System.Text.Json;
internal class Program
{
    static Dictionary<string, TcpClient> clients = new Dictionary<string, TcpClient>();
    private static async Task Main(string[] args)
    {
        TcpListener server = new TcpListener(IPAddress.Any, 8080);
        server.Start();
        Console.WriteLine("Server listening at port 8080");

        while (true)
        {
            var client = await server.AcceptTcpClientAsync();
            Console.WriteLine($"Established connection from {client.Client.RemoteEndPoint}");
            Task.Run(() => HandleClientAsync(client));
        }
    }

    static async void HandleClientAsync(TcpClient client)
    {
        while (client.Connected)
        {
            var stream = client.GetStream();
            var user = await ReadMessageAsync(stream);

            if (!clients.ContainsKey(user.Id)) clients.Add(user.Id, client);

            BroadcastNewUsers(user.Id);
            if (user.ReceiverId != null)
            {
                var receiver = clients.Where(c => c.Key == user.Id).Select(c => c.Value).FirstOrDefault();

                if (receiver == null) await stream.WriteAsync(Encoding.UTF8.GetBytes("User not found\0"));
                else
                {
                    var receiverStream = receiver.GetStream();
                    receiverStream.WriteAsync(Encoding.UTF8.GetBytes(new Message(user).ToString()));
                }
            }


        }
    }

    static void BroadcastNewUsers(string username)
    {
        foreach (var user in clients)
        {
            if (user.Key == user.Key) continue;
            var stream = user.Value.GetStream();
            stream.Write(Encoding.UTF8.GetBytes(username + '\0'));
        }
    }

    private static async Task<User> ReadMessageAsync(NetworkStream stream)
    {
        var data = new List<byte>();
        int currentByte;
        do
        {
            currentByte = stream.ReadByte();
            if (currentByte == '\0') break;
            data.Add((byte)currentByte);
        }
        while (currentByte != '\0');

        return JsonSerializer.Deserialize<User>(data.ToArray());
    }
}