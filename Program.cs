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

        public List<int> GEtCourseWork()
        {
            return this.courseWorks;
        }
    }

    public class Student
    {
        private FullName fullName;
        private DateTime dateOfBirth;
        private double? phoneNumber;
        private Scores scores;

        public Student() : this("Alex", "Mart", "", new DateTime(), 0) { }
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
    private static void Main(string[] args)
    {
        Student st = new Student();
        st.GetScores().AddExam(10);
        st.PrintStudentInfo();

    }
}