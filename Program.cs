using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

internal class Program
{

    class Name
    {
        public string? FirstName { get; set; }
        public string? Surname { get; set; }
        public string? Lastname { get; set; }
    }

    class Place
    {
        public string? Country { get; set; }
        public string? Region { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public int HouseNumber { get; set; }
        public char Korpus { get; set; }
    }

    class Group
    {
        public int Kurs { get; set; }
        public string? GroupName { get; set; }
        public string? Specialization { get; set; }
        public List<Student> Students { get; set; }
        public Schedule Schedule { get; set; }

    }

    class Subject
    {
        public string? TeacherName { get; set; }
        public string? SubjectName { get; set; }
    }

    class Schedule
    {
        List<Subject> Subjects { get; set; }
    }

    class Rating
    {
        public int LessonsVisited { get; set; }
        public int LessonsLate { get; set; }
        public int[]? DzRates { get; set; }
        public int[]? PracticeRates { get; set; }
        public int[]? ExamRates { get; set; }
        public int[]? ZachetRates { get; set; }
        public int ZachetCount { get; set; }
        public double TotalAverageRate { get; set; }
    }

    class Student
    {
        public Name Name { get; set; }
        public Place Place { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime StartDate { get; set; }
        public int GroupNumber { get; set; }
        public Rating Rating { get; set; }
    }

    private static void Main(string[] args)
    {
       
    }
}

