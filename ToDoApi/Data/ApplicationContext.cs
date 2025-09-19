using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;

namespace ToDoApi.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options) { }
 
    public DbSet<TaskItem> Tasks => Set<TaskItem>();
}