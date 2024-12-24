using Microsoft.EntityFrameworkCore;
using hw10.Models;
namespace hw10.Data
{
    public class ApplicationContext: DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

    }
}
