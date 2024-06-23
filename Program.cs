
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using static Program;

internal class Program
{

    public struct FullName
    {
        public String firstName;
        public String lastName;
        public String? fatherName;

        public void PrintFullName()
        {
            Console.Write("{0} {1} {2}", lastName, firstName, fatherName);
            Console.WriteLine();
        }

        public String GetFullName()
        {
            return this.lastName + " " + this.firstName + " " + this.fatherName;
        }
    }
    public class Scores
    {
        private List<int> courseWorks;
        private List<int> exams;
        private List<int> tests;

        public Scores()
        {
            this.courseWorks = new List<int>();
            this.exams = new List<int>();
            this.tests= new List<int>();
        }
        public void PrintScores()
        {
            Console.Write("Course work: ");
            foreach (int item in courseWorks)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            Console.Write("Exams: ");
            foreach (int item in exams)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
            Console.Write("Tests: ");
            foreach (int item in tests)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }

        public void AddExam(int score)
        {
            this.exams.Add(score);
        }

        public void AddTest(int score)
        {
            this.tests.Add(score);
        }

        public void AddCourseWork(int score)
        {
            this.courseWorks.Add(score);
        }

        //accessors
        // getters
        public List<int> GetExams()
        {
            return this.exams;
        }

        public List<int> GetTests()
        {
            return this.tests;
        }

        public List<int> GetCourseWork()
        {
            return this.courseWorks;
        }

        public double AverageScore()
        {
            double averageScore = (exams.Average() + tests.Average() + courseWorks.Average()) / 3;

            return averageScore;
        }
    }
    public class Student : IComparable
    {
        private FullName fullName;
        private DateTime dateOfBirth;
        private double? phoneNumber;
        private Scores scores;
        private uint age;
        public uint Age
        { 
            get
            {
                return this.age;
            }

            set
            {
                if (value >= 18 && value < 100)
                {
                    this.age = value;
                }
                else
                {
                    throw new Exception("error");
                }
            }
        }

        public Student() : this("Alex", "Mart") { }
        public Student(String firstName, String lastName) : this(firstName, lastName, new DateTime()) { }
        public Student(String firstName, String lastName, DateTime dayOfBirth) : this(firstName, lastName, "", dayOfBirth, 0, 18) { }
        public Student(String firstName, String lastName, String? fatherName, DateTime dateOfBirth, double? phoneNumber, uint age)
        {
            this.Age = age;
            SetFullName(firstName, lastName, fatherName);
            SetDateOfBirth(dateOfBirth);
            SetPhoneNumber(phoneNumber);
            SetScores();
        }
    
        // accessors 
        // Setters
        public void SetFullName(String firstName, String lastName, String? fatherName)
        {
            this.fullName.firstName = firstName;
            this.fullName.lastName = lastName;
            this.fullName.fatherName = fatherName;
        }      

        public void SetScores()
        {
            scores = new Scores();
        }

        public void SetDateOfBirth(DateTime dateOfBirth)
        {
            this.dateOfBirth = dateOfBirth;
        }

        public void SetPhoneNumber(double? phoneNumber)
        {
            this.phoneNumber = phoneNumber;
        }

        // Getters
        public FullName GetFullName()
        {
            return this.fullName;
        }

        public Scores GetScores()
        {
            return this.scores;
        }

        public DateTime GetDateOfBirth()
        {
            return this.dateOfBirth;
        }

        public double? GetPhoneNumber()
        {
            return this.phoneNumber;
        }

        public virtual void PrintStudentInfo()
        {
            Console.Write("Full Name: ");
            fullName.PrintFullName();
            Console.WriteLine("Age: "+ this.Age);
            Console.WriteLine("Date of birth: " + this.dateOfBirth);
            Console.WriteLine("Phone number: " + this.phoneNumber);
            Console.WriteLine("Scores:");
            scores.PrintScores();
        }

        public virtual void Study()
        {
            Console.WriteLine("this student is study");
        }

        public virtual void TakeExam()
        {
            Console.WriteLine("This student taking exam");
        }

        public int CompareTo(object? obj)
        {
            Student? student = obj as Student;
            return this.scores.AverageScore().CompareTo(student.scores.AverageScore());
        }

        public class CompareByName : IComparer
        {
            public int Compare(object? left, object? right)
            {
                Student x = (Student)left;
                Student y = (Student)right;

                return x.fullName.lastName.CompareTo(y.fullName.lastName);
            }
        }

        public class CompareByAge : IComparer
        {
            public int Compare(object? left, object? right)
            {
                Student x = (Student)left;
                Student y = (Student)right;

                return x.age.CompareTo(y.age);
            }
        }

        public class CompareByDayOfBirth : IComparer
        {
            public int Compare(object? left, object? right)
            {
                Student x = (Student)left;
                Student y = (Student)right;

                return x.dateOfBirth.CompareTo(y.dateOfBirth);
            }
        }

        public class CompareByScore : IComparer
        {
            public int Compare(object? left, object? right)
            {
                Student x = (Student)left;
                Student y = (Student)right;

                return x.scores.AverageScore().CompareTo(y.scores.AverageScore());
            }
        }

        public static bool operator ==(Student right, Student left) 
        {
            if (right.fullName.GetFullName() == left.fullName.GetFullName() &&
                right.dateOfBirth == left.dateOfBirth &&
                right.phoneNumber == left.phoneNumber &&
                right.scores.AverageScore() == left.scores.AverageScore()
                )
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(Student left, Student right) { return !(right == left); }

        public static bool operator >(Student left, Student right)
        {
            return left.scores.AverageScore() > right.scores.AverageScore();
        }

        public static bool operator<(Student left, Student right){ return !(left > right); }
        public static bool operator true(Student student)
        {
            return student.scores.AverageScore() >= 7;
        }

        public static bool operator false(Student student) 
        {
            return student.scores.AverageScore() < 7; 
        }
    }

    public class Group
    {
        private String name;
        private String specialization;
        private uint number;
        private List<Student> students;

        public uint Course 
        {
            get { return this.Course; }
            set
            {
                if (value >= 1 && value <= 6)
                {
                    this.Course = value;
                }
                else
                {
                    throw new Exception("error");
                }
            }
        }  

        private void SortStudents()
        {
            students.Sort(new Student.CompareByName().Compare);
        }

        public void SortStudents(IComparer comparer)
        {
            students.Sort(comparer.Compare);
        }

        public int FindStudent(Student studentToFind, IComparer comparer)
        {
            int index = 0;
            foreach(Student student in students)
            {
                if (comparer.Compare(student, studentToFind) == 0)
                {
                    return index;
                }
                index++;
            }

            return -1;
        }

        public Group() : this("Default", "build", 0, new List<Student>()) { }
        public Group(String name, String specialization, uint number, List<Student> students)
        {
            SetStudents(students);
            SetName(name);
            SetSpecialization(specialization);
            SetNumber(number);
        }

        public void ShowGroup()
        {
            Console.WriteLine("Name: " + this.name);
            Console.WriteLine("Specialization: " + this.specialization);
            Console.WriteLine("Group number: " + this.number);
            for (int i = 0; i < this.students.Count; i++)
            {
                Console.Write((i + 1) + ". ");
                students[i].GetFullName().PrintFullName();
            }
            Console.WriteLine();
        }

        public void AddStudent(Student student)
        {
            this.students.Add(student);
            this.SortStudents();
        }

        public void TransferStudent(ref Group group, int studentNumber) 
        {
            group.AddStudent(this.students[studentNumber - 1]);
            this.students.RemoveAt(studentNumber - 1);
        }

        public void ExpelStudent()
        {
            //Student student = students.OrderBy(st => st.GetScores().AverageScore()).First();
            int index = 0;
            double min = students[index].GetScores().AverageScore();

            for (int i = 0; i < this.students.Count; i++)
            {
                if (students[i].GetScores().AverageScore() < min)
                {
                    index = i;
                }
                
            }

            this.students.RemoveAt(index);
        }

        // accessors
        // setters
        public void SetName(String name)
        {
            this.name = name;
        }

        public void SetSpecialization(String specialization)
        {
            this.specialization = specialization;
        }

        public void SetNumber(uint number)
        {
            this.number = number;
        }

        public void SetStudents(List<Student> students)
        {
            this.students = students;
            this.SortStudents();
        }

        // getters
        public String GetName()
        {
            return this.name;
        }

        public String GetSpecialization()
        {
            return this.specialization;
        }

        public uint GetNumber()
        {
            return this.number;
        }

        public List<Student> GetStudents()
        {
            return this.students;
        }

        public static bool operator==(Group left, Group right)
        {
            if (left.name == right.name &&
                left.specialization == right.specialization &&
                left.number == right.number &&
                left.students.Equals(right.students)
                )
            {
                return true; 
            }
            return false;
        }

        public static bool operator !=(Group left, Group right) { return !(left == right); }

        public  Student this[int index]
        {
            get { return this.students[index]; }
            set
            {
                if (index >= this.students.Count)
                {
                    this.students.Add(value);
                }
                else
                {
                    this.students[index] = value;
                }
            }
        }
    }

    public class Aspirant : Student
    {
        private string thesisTheme;
        public string ThesisTheme { get { return this.thesisTheme; } set { this.thesisTheme = value; } }
        public Aspirant() : this("Thesis theme") { }
        public Aspirant(string thesisTheme) : this("FirstName", "LastName", thesisTheme) { }
        public Aspirant(string firstName, string lastName, string thesisTheme) : this(firstName, lastName, "", 0, new DateTime(), 18, thesisTheme)  { }
        public Aspirant(string firstName, string lastName, string fatherName, uint phoneNumber, DateTime dateOfBirth, uint age, string thesisTheme) : base(firstName, lastName, fatherName, dateOfBirth, phoneNumber, age)
        {
            this.ThesisTheme = thesisTheme;
        }

        public void DoInternship()
        {
            Console.WriteLine("What is internship???");
        }

        public void DefendThesis()
        {
            Console.WriteLine("defending thesis.");
        }

        public override void PrintStudentInfo()
        {
            base.PrintStudentInfo();
            Console.WriteLine("Thesis theme: " + this.ThesisTheme);
        }

        public override void Study()
        {
            Console.WriteLine("This aspirant is study");
        }

        public override void TakeExam()
        {
            Console.WriteLine("This aspirant taking exam");
        }
    }

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
        group.ShowGroup();
        Random random = new Random();

        for (int i = 0; i < group.GetStudents().Count; i++)
        {
            group.GetStudents()[i].GetScores().AddCourseWork(random.Next(1, 12));
            group.GetStudents()[i].GetScores().AddExam(random.Next(1, 12));
            group.GetStudents()[i].GetScores().AddTest(random.Next(1, 12));

        }

        group.SortStudents(new Student.CompareByScore());
        group.ShowGroup();
        Student newStudent = new Student("One", "test");
        int fst = group.FindStudent(newStudent, new Student.CompareByName());
        group[fst].PrintStudentInfo();
    }
}