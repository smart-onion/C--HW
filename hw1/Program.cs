using System.IO;
using System.Net.Sockets;
using System.Text;
using Shared;
internal class Program
{
    

    private static void Main()
    {
        ConnectToServer("localhost", 8080, Task2);
    }

    static void ConnectToServer(string ip, int port, Request? request = null)
    {
        var client = new TcpClient(ip, port);
        Console.WriteLine($"Connected to the {ip}:{port}");

        try
        {
            NetworkStream stream = client.GetStream();

            request?.Invoke(stream);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
        finally
        {
            client.Close();
        }
    }

    static void Task1(NetworkStream ns)
    {
        Console.WriteLine("1 - Get time from server");
        Console.WriteLine("2 - Get date from server");
        Console.Write("Please choose option: ");

        var action = Console.ReadLine();
        
        byte[] data;

        switch (action)
        {
            case "1":
                data = Encoding.UTF8.GetBytes("TIME");
                break;
            case "2":
                data = Encoding.UTF8.GetBytes("DATE");
                break;
            default:
                data = Encoding.UTF8.GetBytes("ERR");
                break;
        }

        ns.Write(data, 0, data.Length);

        string responseData = Utility.ReadStringFromStream(ns);
        Console.WriteLine("Response: " + responseData.ToString());
    }

    static void Task2(NetworkStream ns)
    {
        Console.Write("Send number to server to get square of it: ");
        var number = Console.ReadLine();
        byte[] data = Encoding.UTF8.GetBytes(number);

        ns.Write(data, 0, data.Length);

        var response = Utility.ReadStringFromStream(ns);

        Console.WriteLine("Result = " + response);
    }
}