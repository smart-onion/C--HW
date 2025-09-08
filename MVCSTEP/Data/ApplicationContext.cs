using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCSTEP.Models;

namespace MVCSTEP.Data;

public class ApplicationContext : IdentityDbContext<User>
{
    public DbSet<Membership> Memberships { get; set; }
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
}