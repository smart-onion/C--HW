using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

internal class Program
{
    public class Student
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Student() : this("ALex", 18) { }

        [JsonConstructor]
        public Student(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }
    }
    public class Group
    {
        public string Name { get; set; }
        public string Specialization { get; set; }
        public int Number { get; set; }
        public List<Student> Students { get; set; }

        public Group () : this("Group", "Test", 1, new List<Student>()){ }

        [JsonConstructor]
        public Group(string name, string specialization, int number, List<Student> students)
        {
            Name = name;
            Specialization = specialization;
            Number = number;
            Students = students;
        }
    }

    private static void Main(string[] args)
    {
        Group gr = new Group();

        gr.Students.Add(new Student());
        gr.Students.Add(new Student());
        gr.Students.Add(new Student());
        gr.Students.Add(new Student());
        gr.Students.Add(new Student());

        var sw = new StreamWriter("./file.json");
        string jsonString = JsonSerializer.Serialize(gr);
        Console.WriteLine(jsonString);
        sw.Write(jsonString);
        sw.Close();

        jsonString = File.ReadAllText("./file.json");
        Console.WriteLine();
        Group gr2 = JsonSerializer.Deserialize<Group>(jsonString);
        Console.WriteLine(gr2);
    }
}