using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCSTEP.Models;

namespace MVCSTEP.Data;

public class ApplicationContext : IdentityDbContext<User>
{
    public DbSet<MVCSTEP.Models.Note> Note { get; set; } = default!;
    public DbSet<Publication> Publication { get; set; } = default!;
    public DbSet<UserFriends> UserFriends { get; set; } = default!;

    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        
        
        base.OnModelCreating(builder);
    }
}