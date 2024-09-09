using HW6;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        using(ApplicationContext db = new ApplicationContext())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            db.Students.Add(new Student { Fio = "Alex", DateOfBirth = new DateTime(1990, 10, 1) });
            db.Courses.Add(new Course { Name = "C#", Description = "Description" });
            db.Courses.Add(new Course { Name = "C++", Description = "Description" });
            db.Instructors.Add(new Instructor { Fio = "Bob" });
            db.SaveChanges();
            db.Enrollments.Add(new Enrollment { CourseId = 1, StudentId = 1, InstructorId = 1, EnrollmentDate = DateTime.Now });
            db.SaveChanges();

            Task1(db);
            Task2(db);
            Task3(db);
            Task4(db);
            Task5(db);
            Task6(db);
            Task7(db);
            Task8(db);
            Task9(db);
            Task10(db);
            Task11(db);
            Task12(db);
            Task13(db);
            Task14(db);
            Task15(db);
        }
    }

    static void Task1(ApplicationContext db)
    {
        var students = db.Enrollments
            .Where(e => e.CourseId == 1)
            .Select(e => e.Student)
            .ToList();

    }

    static void Task2(ApplicationContext db)
    {
        var courses = db.Enrollments
            .Where(e => e.InstructorId == 1)
            .Select(e => e.Course)
            .ToList();
    }

    static void Task3(ApplicationContext db)
    {
        var courses = db.Enrollments
            .Where(e => e.InstructorId == 1)
            .Select(e => new { StudentFio = e.Student.Fio, Course = e.Course })
            .ToList();
    }

    static void Task4(ApplicationContext db)
    {
        var courses = db.Enrollments
            .GroupBy(e => e.Course)
            .Where(e => e.Count() > 5)
            .Select(e=>e.Key)
            .ToList();
    }

    static void Task5(ApplicationContext db)
    {
        var students = db.Students
            .Where(e => DateTime.Now.Year - e.DateOfBirth.Year > 25)
            .ToList();
    }

    static void Task6(ApplicationContext db)
    {
        var avg = db.Students.Average(e => DateTime.Now.Year - e.DateOfBirth.Year);
    }

    static void Task7(ApplicationContext db)
    {
        var min = db.Students.Max(e => e.DateOfBirth);
    }

    static void Task8(ApplicationContext db)
    {
        var courses = db.Enrollments
            .Where(e => e.StudentId == 1)
            .Select(e=>e.Course)
            .Count();
    }

    static void Task9(ApplicationContext db)
    {
        var names = db.Students.Select(e => new { Name = e.Fio }).ToList();
    }

    static void Task10(ApplicationContext db)
    {
        var ord = db.Students
            .GroupBy(e => e.DateOfBirth)
            .ToList();
    }

    static void Task11(ApplicationContext db)
    {
        var students = db.Students
            .OrderBy(e => e.Fio)
            .ToList();
    }

    static void Task12(ApplicationContext db)
    {
        var students = db.Enrollments
            .Select(e => new { Student = e.Student, Course = e.Course })
            .ToList();
    }

    static void Task13(ApplicationContext db)
    {
        var students = db.Enrollments
            .Where(e => e.CourseId != 1)
            .Select(e => e.Student)
            .ToList();
    }

    static void Task14(ApplicationContext db)
    {
        var students = db.Enrollments
            .Where(e => e.CourseId == 1 && e.CourseId == 2)
            .Select(e => e.Student)
            .ToList();
    }

    static void Task15(ApplicationContext db)
    {
        var st = db.Enrollments
            .GroupBy(e => e.Course)
            .Select(e => new { Course = e.Key, StudentsNumber = e.Count() })
            .ToList();
    }
}