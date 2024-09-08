namespace HW51
{
    public class PlaneSpecification
    {
        public int Id { get; set; }
        public string Engine { get; set; }
        public string Length { get; set; }
        public int PlaneId { get; set; }
        public Plane Plane { get; set; }
    }
}
