using System;
using System.Collections;
using System.Xml.Serialization;

[Serializable]
public class Student : IComparable
{
    [Serializable]
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
    [Serializable]
    public class Scores
    {
        private List<int> courseWorks;
        private List<int> exams;
        private List<int> tests;

        public Scores()
        {
            this.courseWorks = new List<int>();
            this.exams = new List<int>();
            this.tests = new List<int>();
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

    public delegate int ExamHandler();
    public delegate void OversleepHandler();
    public delegate void MultiHandler();

    public event ExamHandler OnExam;
    public event OversleepHandler OnOversleep;
    public event MultiHandler OnAbsenteeism;
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


    // Events

    public void PassExam()
    {
        int? score = OnExam?.Invoke();

        if (score < 7)
        {
            Console.WriteLine($"Ops, Your score is {score}.Try next time");
        }
        else if(score >= 7)
        {
            Console.WriteLine($"Your score is {score}.Congratulation!!!");
        }
    }

    public void Oversleep(int time, bool celebrationDay)
    {
        if (time > 9)
        {
            OnOversleep?.Invoke();
            if (celebrationDay) OnAbsenteeism?.Invoke();
        }
    }

    public void SkipLesson()
    {
        OnAbsenteeism?.Invoke();
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
        Console.WriteLine("Age: " + this.Age);
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

    public static bool operator <(Student left, Student right) { return !(left > right); }
    public static bool operator true(Student student)
    {
        return student.scores.AverageScore() >= 7;
    }

    public static bool operator false(Student student)
    {
        return student.scores.AverageScore() < 7;
    }
}
