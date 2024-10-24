using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Database;
namespace Net4
{
    public partial class Form1 : Form
    {
        UdpClient udpClient = new UdpClient(0);
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9001);
        public Form1()
        {
            InitializeComponent();
        }

        private async void SendButton_Click(object sender, EventArgs e)
        {

            byte[] data = Encoding.UTF8.GetBytes(MessageInputBox.Text);

            await udpClient.SendAsync(data, data.Length, endPoint);
        }

        private void HandleServerMessage()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        var result = await udpClient.ReceiveAsync();
                        var part = JsonSerializer.Deserialize<SparePart>(result.Buffer);
                       
                        MessageBox.Show($"{part.Name} price = {part.Price}");
                        
                    
                    }
                    catch (JsonException ex)
                    {
                        MessageBox.Show($"NOT FOUND");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error receiving message: {ex}");
                    }
                }
            });

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HandleServerMessage();
        }
    }
}
