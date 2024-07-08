using System;
using System.Collections;
using System.Xml.Serialization;
[Serializable]
[XmlInclude(typeof(Student))]
public class Group : IEnumerable
{
    private String name;
    private String specialization;
    private uint number;
    private List<Student> students;
    private uint course;
    public int test = 1;
    [XmlElement("Name")]
    public string Name { get => this.name; set { SetName(value); } }
    [XmlElement("Specialization")]
    public string Specialization { get => this.specialization; set { SetSpecialization(value); } }
    [XmlElement("Number")]
    public uint Number { get => this.number; set { SetNumber(value); } }
    [XmlArray("Students")]
    [XmlArrayItem("Student")]
    public List<Student> Students { get => this.students; }
    

    public delegate void ChangeHandler();
    public delegate void PartyHandler();

    public event PartyHandler OnParty;
    public event ChangeHandler OnChangeCourse;
    [XmlElement("Course")]
    public uint Course
    {
        get { return this.course; }
        set
        {
            if (value >= 1 && value <= 6)
            {
                this.course = value;
            }
            else
            {
                throw new Exception("error");
            }
        }
    }
    public void Add(object obj) { }
    public void AssembleGroup()
    {
        Console.WriteLine("Group assembled");
    }

    // Events

    public void ChangeCourse()
    {
        if (OnChangeCourse != null)
        {
            OnChangeCourse();
            Course++;
        }
    }

    public void MakeGroupParty(bool celebrationDay)
    {
        if (celebrationDay)
        {
            OnParty += AssembleGroup;
            OnParty();
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
        foreach (Student student in students)
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

    public IEnumerator GetEnumerator()
    {
        return new GroupEnumerator(this.students);
    }

    public static bool operator ==(Group left, Group right)
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

    public Student this[int index]
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
[Serializable]
public class GroupEnumerator : IEnumerator
{
    private List<Student> students;
    private int position = -1;

    public GroupEnumerator(List<Student> students)
    {
        this.students = students;
    }
    public object Current => this.students[this.position];

    public bool MoveNext()
    {
        this.position++;
        return position < this.students.Count;
    }

    public void Reset()
    {
        this.position = -1;
    }
}