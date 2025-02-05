using AspIdentityHW1.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspIdentityHW1.Data
{
    public class ApplicationContext: IdentityDbContext
    {
        public DbSet<Article> Articles { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }
    }
}
