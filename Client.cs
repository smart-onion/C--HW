namespace HW7
{
    internal class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public List<Order> Orders { get; set; }
    }
}
