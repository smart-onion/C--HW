using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Scheduler
{
    public Scheduler()
    {
       
    }

    public int GetRandomTime()
    {
        return new Random().Next(1, 24);
    }

    public bool GetCelebrationDay()
    {
        int rand = new Random().Next(-1, 2);
        if (rand == 1) return false;
        return true;
    }

    public void NewYear()
    {
        Console.WriteLine("New year - new course");
    }

}
