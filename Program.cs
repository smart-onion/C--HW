using HW7;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using System.Text.RegularExpressions;

internal class Program
{
    private static async Task Main(string[] args)
    {
        // Task 1
        //try
        //{
        //    User? u = await GetUserFromDbAsync(1, 10);
        //    Console.WriteLine(u?.ToString());
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine(ex);
        //}

        // Task 2
        //List<Exception?> exps = new List<Exception?>();
        //exps.Add(await CheckNames(new string[] { "Alex", "Bob", "Sam" }));
        //exps.Add(await CheckNames(new string[] { "Sam", "Bob", "Sam" }));
        //exps.Add(await CheckNames(new string[] { "Rob", "Bob", "Sam" }));

        //foreach (var ex in exps)
        //{
        //    Console.WriteLine(ex?.Message);
        //}

        // Task 3
        ChangeDirectory();
    }


    static async Task<User?> GetUserFromDbAsync(int userId, int timeout)
    {
        using var db = new UserDbContext();
        using var cts = new CancellationTokenSource(timeout);

        CancellationToken token = cts.Token;
        User? user = null;
        try
        {
            // Simulate server timeout error
            Task.Delay(10000);
            user = await db.Users.FirstOrDefaultAsync(u => u.Id == userId, token);
        }
        catch (TaskCanceledException)
        {
            Console.WriteLine("Timeout exceed!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        return user;

    }

    static async Task<Exception?> CheckNames(string[] names)
    {
        return await Task.Run(() =>
        {
            var uniqNames = names.ToHashSet();

            if (uniqNames.Count != names.Length) return new Exception("String contain same names");
            else if (names.Contains("Alex")) return new Exception("Name not allowed");
            else
            {
                foreach (var name in names) Console.WriteLine(name);
                return null;
            }

        });
    }

    static void ChangeDirectory()
    {
        var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
        string[] commands = new string[10];
        var ShowDir = () =>
        {
            foreach(var dir in directory.GetDirectories())
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine(dir.Name);
                Console.ForegroundColor = ConsoleColor.White;
            }
            foreach(var file in directory.GetFiles())
            {
                Console.WriteLine(file.Name);
            }
        };

        while (commands[0] != "exit")
        {
            Console.Write(directory.FullName + "$");
            commands = Console.ReadLine().Split(" ");
            if (commands[0] == "cd")
            {
                if (Path.Exists(commands[1]))
                {
                    Directory.SetCurrentDirectory(Path.GetFullPath(commands[1]));
                    directory = new DirectoryInfo(Path.GetFullPath(commands[1]));
                }
                else
                {
                    Console.WriteLine("Path not exist!");
                }
            }
            else if (commands[0] == "ls")
            {
                ShowDir();
            }
            else if (commands[0] == "pwd") Console.WriteLine(Directory.GetCurrentDirectory());
            else if (commands[0] == "clear") Console.Clear();
        }
    }
}