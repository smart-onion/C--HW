using AuthenticationHW.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationHW.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public ApplicationContext(DbContextOptions options) :base(options) { }
    }
}
