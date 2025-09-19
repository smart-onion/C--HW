using Microsoft.EntityFrameworkCore;
using VotingSystem.API.Models;

namespace VotingSystem.API.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    
    public DbSet<Poll> Polls { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<AnswerOption> AnswerOptions { get; set; }
    public DbSet<Vote> Votes { get; set; }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vote>().Property(e=>e.Timestamp).HasDefaultValueSql("NOW()");
    }
}