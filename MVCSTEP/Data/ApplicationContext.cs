using Microsoft.EntityFrameworkCore;
using MVCSTEP.Models;

namespace MVCSTEP.Data;


public class ApplicationContext: DbContext
{
    public DbSet<Product> Products { get; set; }
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }
}