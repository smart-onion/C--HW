namespace HW6
{
    public class Student
    {
        public int Id { get; set; }
        public string Fio { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IEnumerable<Enrollment> Enrollments { get; set; }
    }
}
