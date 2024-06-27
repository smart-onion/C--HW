using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Teacher
{
    List<Group> groups;

    public Teacher()
    {
        groups = new List<Group>();
    }

    public int TakeExam() 
    {
        return new Random().Next(1, 12);
    }

    public void PunishStudent()
    {
        Console.WriteLine("You get punished :(");
    }

    public void MaskNotPresent()
    {
        Console.WriteLine("Student is absent");
    }

    public void JoinGroup()
    {
        Console.WriteLine("Together with students :)");
    }
}