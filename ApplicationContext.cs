using Microsoft.EntityFrameworkCore;

class ApplicationContext : DbContext
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<ProjectsTable> projectsTables{ get; set; }

    public ApplicationContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=HW41;Trusted_Connection=True;");
    }
}

