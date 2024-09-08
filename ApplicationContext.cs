using Microsoft.EntityFrameworkCore;

namespace HW5
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<GuestEvent> GuestEvents { get; set; }

        public ApplicationContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=HW5;Trusted_Connection=True;");

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Guest>()
                .HasMany(e => e.Events)
                .WithMany(e => e.Guests)
                .UsingEntity<GuestEvent>(e =>
                {
                    e.HasOne(e => e.Guest).WithMany(e => e.GuestEvents).HasForeignKey(e => e.GuestId);
                    e.HasOne(e => e.Event).WithMany(e => e.GuestEvents).HasForeignKey(e => e.EventId);
                });
            
                


            modelBuilder.Entity<Guest>()
                .HasData(
                new Guest
                {
                    Id = -1,
                    Name = "Alex",
                    Age = 20
                },
                new Guest
                {
                    Id = -2,
                    Name = "Bob",
                    Age = 19,
                },
                new Guest
                {
                    Id = -3,
                    Name = "Jon",
                    Age = 21,
                }
                );

            modelBuilder.Entity<Event>()
                .HasData(
                new Event
                {
                    Id = -1,
                    Name = "Jump",
                    Description = "Some description"
                },
                new Event
                {
                    Id = -2,
                    Name = "Walk",
                    Description = "Walk by street"
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
