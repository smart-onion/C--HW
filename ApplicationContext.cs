using Microsoft.EntityFrameworkCore;

namespace HW51
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Plane> Planes { get; set; }
        public DbSet<PlaneSpecification> PlaneSpecifications { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Country> Countries { get; set; }

        public ApplicationContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=HW5;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airport>()
                .HasOne(e => e.Country)
                .WithMany(e => e.Airports)
                .HasForeignKey(e => e.CountryId);

            modelBuilder.Entity<PlaneSpecification>()
                .HasOne(e => e.Plane)
                .WithOne(e => e.PlaneSpecification);

            modelBuilder.Entity<Plane>()
                .HasOne(e => e.Airport)
                .WithMany(e => e.Planes)
                .HasForeignKey(e => e.AirportId);



            base.OnModelCreating(modelBuilder);
        }
    }
}
