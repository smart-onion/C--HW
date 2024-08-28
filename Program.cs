using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using HW1;
using System;
using System.Reflection;
internal class Program
{
    private static void Main(string[] args)
    {
        Task1();
        Task2();
    }

    static void Task1()
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("application.json");
        var config = builder.Build();

        string connectionString = config.GetConnectionString("DefaultConnection");
        var optionBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        var options = optionBuilder.UseSqlServer(connectionString).Options;

        using (ApplicationContext db = new ApplicationContext(options))
        {
            TrainStoreManager.SetDatabase(db);
            TrainStoreManager.AddTrain(new Train
            {
                UniqNumber = "A12",
                Model = "model",
                Company = "Company",
                numberOfCarriage = 10,
                Cargo = "cargo"
            });

            TrainStoreManager.EditTrainById(0, new Train { Cargo = "ttt", numberOfCarriage = 100 });
            var t = TrainStoreManager.GetTrainById(2);

            TrainStoreManager.RemoveTrainById(2);
        }



    }

    static void Task2() // Reverse Engineering
    {
        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("application.json");
        var config = builder.Build();

        string connectionString = config.GetConnectionString("ReverceDB");
        var optionBuilder = new DbContextOptionsBuilder<InventoryContext>();
        var options = optionBuilder.UseSqlServer(connectionString).Options;

        using (InventoryContext db = new InventoryContext(options))
        {
            var products = db.Equipment.ToList();
            ShowTable<Equipment>(products);

            db.Equipment.Add(new Equipment
            {
                Name = "fromC#",
                Model = "new Model",
                Category = " new Category",
                Quantity = 10
            });

            db.SaveChanges();

            ShowTable<Equipment>(db.Equipment.ToList());

        }
    }

    static void ShowTable<T>(List<T> list)
    {
        Type objType= typeof(T);
        foreach (var item in list)
        {
            foreach (var eq in objType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                Console.Write(eq.GetValue(item) + " ");
            }
            Console.WriteLine();
        }
    }
}