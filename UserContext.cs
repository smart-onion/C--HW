using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Threading.Channels;

namespace HW4
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }

        public UserContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=HW4;Trusted_Connection=True;");
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserSettings>()
            .HasOne(e => e.User)
            .WithOne(e => e.UserSettings)
            .HasForeignKey<UserSettings>(e => e.UserId)
            .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
