using HW4;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

internal class Program
{
    private static void Main(string[] args)
    {
        using (UserContext db = new UserContext())
        {
            //db.Database.EnsureCreated();
            //db.Users.Add(new User
            //{
            //    Name = "Og",
            //    Age = 31
            //});

            //db.UserSettings.Add(new UserSettings
            //{
            //    Role = Role.GUEST,
            //    Description = "No info",
            //    WorkingHours = 10,
            //    UserId = 4
            //});
            //db.SaveChanges();

            //var user = db.Users.Include(e => e.UserSettings).ToList();
            var user = db.Users.Include(e => e.UserSettings).FirstOrDefault(e => e.Id == 2);
            
            db.Users.Remove(new User { Id = 4 });
            db.SaveChanges();
        }

    }
}