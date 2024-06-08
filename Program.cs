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
    public class Student
    {
        private FullName fullName;
        private DateTime dateOfBirth;
        private double? phoneNumber;
        private Scores scores;

        public Student() : this("Alex", "Mart") { }
        public Student(String firstName, String lastName) : this(firstName, lastName, new DateTime()) { }
        public Student(String firstName, String lastName, DateTime dayOfBirth) : this(firstName, lastName, "", dayOfBirth, 0) { }
        public Student(String firstName, String lastName, String? fatherName, DateTime dateOfBirth, double? phoneNumber)
        {
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

        public void PrintStudentInfo()
        {
            Console.Write("Full Name: ");
            fullName.PrintFullName();
            Console.WriteLine("Date of birth: " + this.dateOfBirth);
            Console.WriteLine("Phone number: " + this.phoneNumber);
            Console.WriteLine("Scores:");
            scores.PrintScores();
        }

    }

    public class Group
    {
        private String name;
        private String specialization;
        private uint number;
        private List<Student> students;

        private void SortStudents()
        {
            students.Sort((st1, st2) => st1.GetFullName().lastName.CompareTo(st2.GetFullName().lastName));
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
        Group group2 = new Group();
        group.TransferStudent(ref group2, 2);
        group.ShowGroup();
        group2.ShowGroup();

        Random random = new Random();

        for (int i = 0; i < group.GetStudents().Count; i++)
        {
            group.GetStudents()[i].GetScores().AddCourseWork(random.Next(1, 12));
            group.GetStudents()[i].GetScores().AddExam(random.Next(1, 12));
            group.GetStudents()[i].GetScores().AddTest(random.Next(1, 12));

        }

        group.ExpelStudent();
        group.ShowGroup();

    }
}