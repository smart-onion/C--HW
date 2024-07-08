
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using static Program;

internal class Program
{

    private static void Main(string[] args)
    {
        Student student = new Student("student1", "st1");
        Student studen2 = new Student("student2", "st2");
        Student studen3 = new Student("student3", "st3");
        Student studen4 = new Student("student4", "st4");
        Student studen5 = new Student("student5", "st5");
        Student studen6 = new Student("student6", "st6");
        Group group = new Group();

        group.AddStudent(student);
        group.AddStudent(studen6);
        group.AddStudent(studen2);
        group.AddStudent(studen3);
        group.AddStudent(studen4);
        group.AddStudent(studen5);

        group.ShowGroup();

        var s = new XmlSerializer(typeof(Group));

        TextWriter sw = new StreamWriter("./file.xml");

        s.Serialize(sw, group);
        sw.Close();
    }
}