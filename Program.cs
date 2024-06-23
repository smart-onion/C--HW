
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using static Program;

internal class Program
{

   
  
   
    private static void Main(string[] args)
    {
        
        Student st = new Student();
        Student st1 = new Student("Some", "test");
        Student st2 = new Student("test2", "Alfa");
        Student st3 = new Student();
        List<Student> l = new List<Student> { st, st1, st2, st3 };
        Group group = new Group();
        group.SetStudents(l);
        group.AddStudent(new Student("Name", "Bravo"));
        Console.WriteLine();
        Random random = new Random();

        foreach (Student sd in group)
        {
            sd.GetScores().AddCourseWork(random.Next(1, 12));
            sd.GetScores().AddExam(random.Next(1, 12));
            sd.GetScores().AddTest(random.Next(1, 12));

        }
        group.ShowGroup();

        foreach (Student sd in group)
        {
            sd.PrintStudentInfo();
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}