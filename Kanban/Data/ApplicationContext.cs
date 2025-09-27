using Kanban.Model;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Data;

public class ApplicationContext : DbContext
{
    public DbSet<Board> Boards { get; set; }
    public DbSet<List> Lists { get; set; }
    public DbSet<Card> Cards { get; set; }

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Database.Migrate();
    }
}