namespace HW51
{
    public class Plane
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PlaneSpecification PlaneSpecification { get; set; }
        public int AirportId { get; set; }
        public Airport Airport { get; set; }
    }
}
