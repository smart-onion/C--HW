using System.Text.Json.Serialization;

namespace LocalLibrary
{
    public class Order
    {
        public string Id { get; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public bool isReady { get; set; }
        public Order() { }
        [JsonConstructor]
        public Order (string id, string name, bool isReady)
        {
            Id = id;
            Name = name;
            this.isReady = isReady;
        }

        public override string ToString()
        {
            return $"{Name} | Is Ready: {isReady}";
        }
    }
}
