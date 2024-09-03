using Microsoft.EntityFrameworkCore;

namespace HW3
{
    public class UserContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public UserContext() { }
        public UserContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=UserDB;Trusted_Connection=True;TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e =>
            {
                e.HasIndex(p => p.Username).IsUnique();
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
