using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCSTEP.Models;

namespace MVCSTEP.Data;

public class ApplicationContext : IdentityDbContext<User>
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Publication> Publications { get; set; }
    public DbSet<Membership> Memberships { get; set; }
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Publication>()
            .HasMany<Category>(s => s.Categories)
            .WithMany(c => c.Publications)
            .UsingEntity(e => e.ToTable("PublicationCategoryRelations"));
 
        modelBuilder.Entity<Publication>().Property(e => e.TotalViews).HasDefaultValue(1);
        modelBuilder.Entity<Publication>().Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
 
        base.OnModelCreating(modelBuilder);
    }
}