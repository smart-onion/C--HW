using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using LocalLibrary;

namespace TcpClient
{
    public partial class Form1 : Form
    {

        System.Net.Sockets.TcpClient tcpClient;
        List<Order> orders = new List<Order>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tcpClient = new System.Net.Sockets.TcpClient("localhost", 8080);
            Task.Run(() => ReceiveMessage());
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void NewOrderButton_Click(object sender, EventArgs e)
        {
            var stream = tcpClient.GetStream();
            stream.WriteByte(1);
            Order order = new Order() { Name = OrderNameBox.Text };
            orders.Add(order);
            await SendOrderAsync(order);
            UpdateOrderListView();

        }

        private async void EditOrderButton_Click(object sender, EventArgs e)
        {
            var stream = tcpClient.GetStream();
            stream.WriteByte(2);
            var order = orders.FirstOrDefault(o => o.Name == OrderNameBox.Text);
            if (order == null) MessageBox.Show("Order not exist");
            else
            {
                await SendOrderAsync(order);
                order.Name = OrderNameBox.Text;
                UpdateOrderListView();
            }
        }

        private async void RemoveOrderButton_Click(object sender, EventArgs e)
        {
            var stream = tcpClient.GetStream();
            stream.WriteByte(3);
            var order = orders.FirstOrDefault(o => o.Name == OrderNameBox.Text);
            if (order == null) MessageBox.Show("Order not exist");
            else
            {
                await SendOrderAsync(order);
                orders.Remove(order);
                UpdateOrderListView();
            }
        }

        private void ReceiveMessage()
        {
            var stream = tcpClient.GetStream();
            while (tcpClient.Connected)
            {
                var data = new List<byte>();
                int currentByte = stream.ReadByte();
                bool isOrder = false;
                if (currentByte == 5) isOrder = true;
                do
                {
                    currentByte = stream.ReadByte();
                    if (currentByte == '\0') break;
                    data.Add((byte)currentByte);
                } while (true);
                if (isOrder)
                {
                    var order = JsonSerializer.Deserialize<Order>(data.ToArray());
                    orders.RemoveAll(o => o.Id == order.Id);
                    UpdateOrderListView();
                }
                else MessageBox.Show(Encoding.UTF8.GetString(data.ToArray()));
            }
        }

        private async Task SendOrderAsync(Order order)
        {
            var stream = tcpClient.GetStream();


            var jsonString = JsonSerializer.Serialize(order);
            jsonString += '\0';
            var data = Encoding.UTF8.GetBytes(jsonString);
            await stream.WriteAsync(data);
        }

        private void UpdateOrderListView()
        {
            OrderListBox.Items.Clear();
            foreach (var order in orders)
            {
                OrderListBox.Items.Add(order);
            }
        }
    }
}
