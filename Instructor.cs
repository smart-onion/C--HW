namespace HW6
{
    public class Instructor
    {
        public int Id { get; set; }
        public string Fio { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
}
