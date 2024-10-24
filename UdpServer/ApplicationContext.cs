using Microsoft.EntityFrameworkCore;
namespace Database
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<SparePart> spareParts { get; set; }

        public  ApplicationContext() 
        { 
            Database.EnsureDeleted();
            Database.EnsureCreated();
            spareParts.AddRangeAsync(GetSpareParts());
            SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Network4;Trusted_Connection=True;");
        }

        public static List<SparePart> GetSpareParts()
        {
            return new List<SparePart>
                {
                    new SparePart {Name = "Processor", Price = 120.99},
                    new SparePart {Name = "RAM", Price = 89.99},
                    new SparePart {Name = "MatherBoard", Price = 99.99},
                    new SparePart {Name = "SSD", Price = 60.99},
                    new SparePart {Name = "HDD", Price = 50.99},
                };

        }
    }

    
}
