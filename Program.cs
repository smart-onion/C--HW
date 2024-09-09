using Microsoft.EntityFrameworkCore;

internal class Program
{
    private static void Main(string[] args)
    {
        using (ApplicationContext db = new ApplicationContext())
        {

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            db.Menus.AddRange(
                new Menu { Name = "File" },
                new Menu { Name = "Edit"},
                new Menu { Name = "View"}
                );
            db.SaveChanges();

            db.Menus.FirstOrDefault(e => e.Name == "File")
                .Childs.AddRange(new List<Menu> {
                new Menu { Name = "Open" },
                new Menu { Name = "Save"},
                new Menu { Name = "Save As", Childs = new List<Menu> { new Menu { Name = "To Hard-drive.." }, new Menu { Name = "To online-drive.."} } }
                });
            db.SaveChanges();

            var all = db.Menus.ToList();
        }
    }
}

public class Menu
{
    public int id { get; set; }
    public string Name { get; set; }
    public int? ParentMenuId { get; set; }
    public Menu? ParentMenu { get; set; }
    public List<Menu> Childs { get; set; } = new();
}

public class ApplicationContext : DbContext
{
    public DbSet<Menu> Menus { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=HW5;Trusted_Connection=True;");
    }
}