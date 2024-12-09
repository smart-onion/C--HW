namespace hw8.Models
{
    public class Product
    {
        static int id = 0;
        public int Id { get;set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public Product()
        {
            Id = ++id;
        }
    }
}
