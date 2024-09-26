using System.Windows;
using System.Windows.Input;

internal class Program
{
    static event Func<bool> OnThreadHandler;
    static List<char> threadLocalString = new List<char>();
    static int threadLocalInt = 0;
    private static void Main(string[] args)
    {
        //Task1();
        Task2();
    }
    static void Temperature()
    {
        var rand = new Random();
        while (OnThreadHandler.Invoke())
        {
            //Console.Clear();
            Console.WriteLine("Current temperature from Thread {0}: {1} degree", rand.Next(0, 100), Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(rand.Next(1000, 1500));
        }
        
    }

    static void Task1()
    {
        List<Thread> threads = new List<Thread>
        {
        new Thread(Temperature),
        new Thread(Temperature),
        new Thread(Temperature),
        new Thread(Temperature),
        };
        OnThreadHandler += () => true;
        foreach (var thread in threads)
        {
            thread.Start();
        }
        Thread.Sleep(10000);
        OnThreadHandler += () => false;

        Console.WriteLine("All threads been finished");
    }

    static void Task2()
    {
        Thread thread = new(WordChecker);
        OnThreadHandler += () => true;
        thread.Start();
        while (true)
        {
            DisplayString();
            string input = Console.ReadLine();
            threadLocalString.AddRange(input);
            threadLocalString.Add(' ');
            DisplayString();

        }
    }
    static void DisplayString()
    {
        Console.Clear();
        Console.WriteLine(ListToString(threadLocalString));
        Console.WriteLine("Words match: {0}", threadLocalInt);
    }
    static string ListToString(List<char> list)
    {
        return new string(list.ToArray());
    }

    static void WordChecker()
    {
       while (OnThreadHandler.Invoke())
       {
            int start = 0;
            int end = 1;
            threadLocalInt = 0;
            for (int i = 0; end <= threadLocalString.Count - 1; i++)
            {
                if (threadLocalString[end] == ' ')
                {

                    if (threadLocalString[start] == threadLocalString[end - 1])
                    {
                        threadLocalInt++;
                    }
                    start = ++end;
                }
                end++;
            }
            Thread.Sleep(100);
        }
    }

 
}