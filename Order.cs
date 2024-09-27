namespace HW7
{
    internal class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public string Address { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}
