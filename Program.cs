
using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using static Program;

internal class Program
{

    private static void Main(string[] args)
    {
        Student student = new Student();
        Teacher teacher = new Teacher();
        Scheduler schedule = new Scheduler();
        Group group = new Group();

        student.OnAbsenteeism += teacher.MaskNotPresent;
        student.OnOversleep += teacher.PunishStudent;
        student.OnExam += teacher.TakeExam;

        student.Oversleep(schedule.GetRandomTime(), schedule.GetCelebrationDay());
        student.PassExam();
        student.SkipLesson();

        group.OnChangeCourse += schedule.NewYear;
        group.OnParty += teacher.JoinGroup;

        group.ChangeCourse();

        group.MakeGroupParty(schedule.GetCelebrationDay());
    }
}