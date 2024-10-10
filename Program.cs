using Microsoft.Win32;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;

internal class Program
{
    static string regPath = @"Software\MyApp";

    private static void Main(string[] args)
    {
        // Task 1
        //Task1();

        // Task 2
        //Task2();

        // Task 3
        Task3();
    }

    static void Task1()
    {
        Console.WriteLine("Welcome to MyApp");

        if (!RegEntryIsExist(regPath))
        {
            Console.Write("Please Enter Your Name: ");
            WriteRegistryValue<string>(regPath, "name", Console.ReadLine());
            Console.Write("Please enter your age: ");
            WriteRegistryValue<int>(regPath, "age", Convert.ToInt32(Console.ReadLine()));
            Console.Write("Please enter your email: ");
            WriteRegistryValue<string>(regPath, "email", Console.ReadLine());
        }

        UserProperties userProperties = new UserProperties()
        {
            Name = ReadRegistryValue<string>(regPath, "name", "User"),
            Age = ReadRegistryValue<int>(regPath, "age", 18),
            Email = ReadRegistryValue<string>(regPath, "email", "")
        };

        PrintUserProp(userProperties);
    }
    static bool RegEntryIsExist(string regPath)
    {
        using (RegistryKey key = Registry.CurrentUser.OpenSubKey(regPath))
        {
            if (key != null) return true;
            else return false;
        }
    }

    static void PrintUserProp(UserProperties us)
    {
        Console.WriteLine($"Name:  {us.Name}");
        Console.WriteLine($"Age:   {us.Age}");
        Console.WriteLine($"Email: {us.Email}");
    }

    static T ReadRegistryValue<T>(string keyPath, string valueName, T defaultValue)
    {
        using (RegistryKey key = Registry.CurrentUser.OpenSubKey(keyPath))
        {
            if (key != null)
            {
                object value = key.GetValue(valueName);
                if (value != null)
                {
                    return (T)Convert.ChangeType(value, typeof(T));
                }
            }
        }
        return defaultValue;
    }

    static void WriteRegistryValue<T>(string keyPath, string valueName, T value)
    {
        using (RegistryKey key = Registry.CurrentUser.CreateSubKey(keyPath))
        {
            if (key != null)
            {
                key.SetValue(valueName, value);
            }
        }
    }

    unsafe static void Task2()
    {
        double val = 10;

        byte* ptr = (byte*)&val;

        *(ptr++) = 1;
        *(ptr++) = (byte)'A';
        *(ptr++) = 0;
        *(ptr++) = 2;
        *(ptr++) = 0;
        *(ptr++) = 0;
        *(ptr++) = 0;
        *(ptr) = 3;

        Console.WriteLine(val.ToString());

        

        
    }

    unsafe static void Task3()
    {
        Random rnd = new Random();

        int first = rnd.Next();
        int second = rnd.Next();
        int third = 0;

        short* sptr = (short*)&second;

        sptr++;

        short* tptr = (short*)&third;

        *tptr++ = *(short*)&first;
        *tptr = *sptr;

        ShowMemory<int>(first);
        ShowMemory<int>(second);
        ShowMemory<int>(third);

    }

    unsafe static void ShowMemory<T>(T value)
    {
        byte* ptr = (byte*)&value;
        Console.Write($"Value = {value}. Memory: ");
        for (int i = 0; i < sizeof(T);i++)
        {
            Console.Write($"{*(ptr++):X2} ");
        }
        Console.WriteLine();

    }
}