using Microsoft.EntityFrameworkCore;
using MVCSTEP.Models;

namespace MVCSTEP.Data;


public class ApplicationContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<UpdateBookRecord> UpdateBookRecords { get; set; }
 
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "Max", Email = "max@gmail.com" },
            new User { Id = 2, Name = "Tom", Email = "tom@example.com" },
            new User { Id = 3, Name = "Anna", Email = "anna@example.com" },
            new User { Id = 4, Name = "John", Email = "john@gmail.com" },
            new User { Id = 5, Name = "Marry", Email = "marry@example.com" }
        );
    }
}