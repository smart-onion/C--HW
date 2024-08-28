using Microsoft.EntityFrameworkCore;

namespace HW1
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Train> Train { get; set; }
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

    }
}
