using HW51;
using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        using (ApplicationContext db = new())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            Task1(db);
            Task2(db);
            Task3(db);
            Task4(db);
            Task5(db);
        }
    }

    static void Task1(ApplicationContext db)
    {
        db.Countries.Add(new Country
        {
            Name = "UK"
        });
        db.SaveChanges();
    }

    static void Task2(ApplicationContext db)
    {
        db.Airports.Add(new Airport
        {
            Name = "SomePort",
            CountryId = 1
        });
        db.SaveChanges();
    }

    static void Task3(ApplicationContext db)
    {
        db.Planes.Add(new Plane
        {
            Name = "NewPlane",
            AirportId = 1
        });
        db.SaveChanges();
    }

    static void Task4(ApplicationContext db)
    {
        var all = db.Planes.Include(e => e.PlaneSpecification).Include(e => e.Airport).ThenInclude(e => e.Country).ToList();
    }

    static void Task5(ApplicationContext db)
    {
        var allC = db.Countries.Include(e => e.Airports).ThenInclude(e => e.Planes).ThenInclude(e => e.PlaneSpecification).ToList();
        var allA = db.Airports.Include(e => e.Country).Include(e => e.Planes).ThenInclude(e => e.PlaneSpecification).ToList();
    }
}