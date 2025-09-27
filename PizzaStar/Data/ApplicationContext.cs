using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PizzaStar.Models;

namespace PizzaStar.Data;

public class ApplicationContext : IdentityDbContext<User>
{
    public DbSet<Category> Categories { get; set; } = null!;
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }
}