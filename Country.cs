namespace HW51
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Airport> Airports { get; set; }
    }
}
