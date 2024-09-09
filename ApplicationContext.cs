using Microsoft.EntityFrameworkCore;

namespace HW6
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students{ get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Enrollment> Enrollments  { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=HW5;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>(e =>
            {
                e.HasOne(e => e.Course).WithMany(e => e.Enrollments).HasForeignKey(e => e.CourseId);
                e.HasOne(e => e.Student).WithMany(e => e.Enrollments).HasForeignKey(e => e.StudentId);
                e.HasOne(e => e.Instructor).WithMany(e => e.Enrollments).HasForeignKey(e => e.InstructorId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
