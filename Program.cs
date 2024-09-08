using HW5;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        using (ApplicationContext db = new())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            db.Guests.Add(new Guest { Name = "Roni", Age = 40 });
            db.Events.Add(new Event { Name = "Fly", Description = "Some description" });
            db.SaveChanges();
            Task1(db);
            Task2(db);
            Task3(db);
            Task4(db);
            //Task5(db);
            Task6(db);

        }
    }

    static void Task1(ApplicationContext db)
    {
        var jump = db.Events.FirstOrDefault(e => e.Id == 1);
        var alex = db.Guests.FirstOrDefault(e => e.Id == 1);

        db.GuestEvents.Add(new GuestEvent
        {
            GuestId = alex.Id,
            EventId = jump.Id,
            Roles = Roles.COMMON
        });

        db.SaveChanges();

    }

    static void Task2(ApplicationContext db)
    {
        var gs = db.GuestEvents.Include(e => e.Guest).Where(e => e.Id == 1).ToList();
    }

    static void Task3(ApplicationContext db)
    {
        var guest = db.GuestEvents.FirstOrDefault(e => e.Id == 1);
        guest.Roles = Roles.SPEAKER;
        db.SaveChanges();
    }

    static void Task4(ApplicationContext db)
    {
        var events = db.GuestEvents.Include(e => e.Event).Where(e => e.GuestId == 1).ToList();
    }

    static void Task5(ApplicationContext db)
    {
        var even = db.Events.FirstOrDefault(e => e.Id == 1);
        var guest = db.Guests.FirstOrDefault(e => e.Id == 1);

        even.Guests.Remove(guest);
        db.SaveChanges();
    }

    static void Task6(ApplicationContext db)
    {
        var events = db.GuestEvents.Include(e => e.Event).Where(e => e.Roles == Roles.SPEAKER);
    }
}