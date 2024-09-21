using System.Diagnostics;
using System.Runtime.InteropServices;

internal class Program
{
    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool MessageBeep(uint uType);

    [DllImport("kernel32.dll")]
    public static extern bool Beep(int frequency, int duration);

    private static void Main(string[] args)
    {
        //Task1();
        //Task2();
        //Task3();
        //Task4();
        //Task5();
    }

    static void Task1()
    {
        Process newProcess = new Process();
        newProcess.StartInfo.FileName = "cmd.exe";
        newProcess.StartInfo.Arguments = "/C echo Child process started";
        int exitCode = 0;
        using (newProcess)
        {
            newProcess.Start();
            newProcess.WaitForExit();
            exitCode = newProcess.ExitCode;
        }
        Console.WriteLine($"Child process exit with status code: {exitCode}");

    }

    enum Notes
    {
     C = 261,
     D = 294,
     E = 329,
     F = 349,
     G = 392,
     A = 440,
     B = 493,
     C1 = 523
    }

    static void Task2()
    {
        Beep((int)Notes.C, 500);
        Beep((int)Notes.C, 500);
        Beep((int)Notes.G, 500);
        Beep((int)Notes.G, 500);
        Beep((int)Notes.A, 500);
        Beep((int)Notes.A, 500);
        Beep((int)Notes.G, 1000);

        Thread.Sleep(500);

        Beep((int)Notes.F, 500);
        Beep((int)Notes.F, 500);
        Beep((int)Notes.E, 500);
        Beep((int)Notes.E, 500);
        Beep((int)Notes.D, 500);
        Beep((int)Notes.D, 500);
        Beep((int)Notes.C, 1000);

        Thread.Sleep(1000);

        MessageBeep(1);
    }

    static void Task3()
    {
        Process process = new Process();
        process.StartInfo.FileName = "notepad.exe";

        process.Start();

        Console.WriteLine("Choose ection:");
        Console.WriteLine("1 - Kill process");
        Console.WriteLine("2 - Wait till process finished");
        var action = Console.ReadLine();
        switch (action)
        {
            case "1":
                process.Kill(true);
                process.WaitForExit();
                break;
            case "2":
                process.WaitForExit();
                int code = process.ExitCode;
                Console.WriteLine("Exit with process: " + code);
                break;
            default:
                break;
        }
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    public static extern int MessageBox(IntPtr hWnd, String text, String caption, uint type);

    static void Task4()
    {
        int next = 1;
        while (next != 0)
        {
            Console.Clear();
            Console.Write("Enter any number between 0 and 100: ");
            int number = Convert.ToInt32(Console.ReadLine());

            int guessNumber = new Random().Next(0, 101);

            MessageBox(IntPtr.Zero, $"Your number = {guessNumber}", "MessageBox Title", 0);

            Console.Write("You want to continue? 1/0:");
            next = Convert.ToInt32(Console.ReadLine());
        }
       
    }

    static void Task5()
    {
        Console.WriteLine("Choose application to run: ");
        Console.WriteLine("1 - Notepad");
        Console.WriteLine("2 - Calculator");
        Console.WriteLine("3 - Paint");
        Console.WriteLine("4 - your own");

        string action = Console.ReadLine();
        string app = "";
        switch (action)
        {
            case "1":
                app = "notepad.exe";
                break;
            case "2":
                app = "calc.exe";
                break;
            case "3":
                app = "mspaint.exe";
                break;
            case "4":
                Console.Write("Enter name of app: ");
                app = Console.ReadLine();
                break;
            default:
                break;
        }

        Process process = new Process();

        process.StartInfo.FileName = app;

        process.Start();
    }

}

