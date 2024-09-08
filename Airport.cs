namespace HW51
{
    public class Airport
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public Country? Country { get; set; }
        public IEnumerable<Plane> Planes { get; set; }
        
    }
}
