using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCSTEP.Core.Entities;

namespace MVCSTEP.Infrastructure.Database_Context;

public class ApplicationContext: IdentityDbContext<User>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }
    public DbSet<Product> Products { get; set; }
    public DbSet<Review> Reviews { get; set; }
}