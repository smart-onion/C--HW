namespace HW7
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}
