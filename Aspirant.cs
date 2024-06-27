using System;
using System.Collections;
public class Aspirant : Student
{
    private string thesisTheme;
    public string ThesisTheme { get { return this.thesisTheme; } set { this.thesisTheme = value; } }
    public Aspirant() : this("Thesis theme") { }
    public Aspirant(string thesisTheme) : this("FirstName", "LastName", thesisTheme) { }
    public Aspirant(string firstName, string lastName, string thesisTheme) : this(firstName, lastName, "", 0, new DateTime(), 18, thesisTheme) { }
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
