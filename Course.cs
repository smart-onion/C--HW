namespace HW6
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
}
