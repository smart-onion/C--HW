using LocalLibrary;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

internal class Program
{
    private static async Task Main(string[] args)
    {
        User user = new User();
        using (TcpClient tcpClient = new TcpClient("localhost", 8080))
        {
            Console.WriteLine("Conneced to message server!");
            Console.Write("Enter your unique username: ");
            user.Id = Console.ReadLine();
            Console.Write("Enter Your Name: ");
            user.Name = Console.ReadLine();
            var stream = tcpClient.GetStream();
            Task.Run(() => ReceiveMessage(stream));

            await stream.WriteAsync(UserToByteArray(user));

            Console.WriteLine($"Welcome: {user.Name}");
            do
            {

                Console.Write("Enter username to start chating: ");
                user.ReceiverId = Console.ReadLine();
                Console.WriteLine("To exit chat write 'exit'...");

                do
                {
                    Console.Write($"Message to {user.ReceiverId}: ");
                    user.Message = Console.ReadLine();

                    var data = UserToByteArray(user);

                    await stream.WriteAsync(data);
                }
                while (user.Message != "exit");
            }
            while (user.Message != "quit");
        }
    }

    static byte[] UserToByteArray(User user)
    {
        var userString = JsonSerializer.Serialize(user);
        userString += '\0';
        return Encoding.UTF8.GetBytes(userString);
    }

    static void ReceiveMessage(NetworkStream stream)
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
        Console.WriteLine(Encoding.UTF8.GetString(data.ToArray()));
    }
}