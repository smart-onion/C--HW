using System.Text.Json.Serialization;

namespace Database
{
    internal class SparePart
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public SparePart() { }

        [JsonConstructor]
        public SparePart(int Id, string Name, double Price)
        {
            this.Id = Id;
            this.Name = Name;
            this.Price = Price;
        }
    }
}
