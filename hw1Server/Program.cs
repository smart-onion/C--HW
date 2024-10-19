using Shared;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class Program
{
    private static void Main(string[] args)
    {
        StartServer("0.0.0.0", 8080, Task2);
    }

    public static void StartServer(string ip, int port, Request? request = null)
    {
        IPAddress ipAddress = IPAddress.Parse(ip);
        TcpListener listener = new TcpListener(ipAddress, port);
        try
        {
            listener.Start();
            Console.WriteLine("Server started...");

            while (true)
            {
                TcpClient? client = listener.AcceptTcpClient();
                
                if (client != null) Console.WriteLine($"{client.Client.RemoteEndPoint} has been connected.");

                NetworkStream stream = client.GetStream();

                request?.Invoke(stream);

                client.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
        finally
        {
            listener.Stop();
        }
    }

    static void Task1(NetworkStream stream)
    {
        var req = Utility.ReadStringFromStream(stream);
        byte[] data = new byte[256];

        switch (req)
        {
            case "DATE":
                data = Encoding.UTF8.GetBytes(DateTime.Now.ToShortDateString());
                break;
            case "TIME":
                data = Encoding.UTF8.GetBytes(DateTime.Now.TimeOfDay.ToString());
                break;
            default:
                data = Encoding.UTF8.GetBytes("Bad request");
                break;
        }

         stream.Write(data, 0, data.Length);
    }

    static async void Task2(NetworkStream stream)
    {
        var req = Utility.ReadStringFromStream(stream);
        int result = Convert.ToInt32(req) * Convert.ToInt32(req);
        byte[] data = Encoding.UTF8.GetBytes(result.ToString());
        await stream.WriteAsync(data,0, data.Length);
    }
}