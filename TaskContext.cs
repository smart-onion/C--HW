using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace HW3
{
    public class TaskContext : DbContext
    {
        DbSet<HW3.Task> Tasks { get; set; }
        public TaskContext() { }
        public TaskContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=(localdb)\\mssqllocaldb;Database=TaskDB;Trusted_Connection=True;TrustServerCertificate=True";
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HW3.Task>(task =>
            {
                
                task.HasKey(t => t.Id); 
                task.Property(t => t.Id).ValueGeneratedOnAdd();

                task.Property(p => p.Name).IsRequired().HasMaxLength(20);
                task.HasIndex(p => p.Name).IsUnique();
                task.Property(p => p.Description).HasMaxLength(120);
                task.HasIndex(p => p.CreationTime);
                task.Property(p => p.CreationTime).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
                task.Property(p => p.DeadlineTime).IsRequired();
                task.HasCheckConstraint("CK_Task_Deadline", "[DeadlineTime] >= [CreationTime]");

                base.OnModelCreating(modelBuilder);


                task.HasData(
                    new Task
                    {
                        Id = -1,
                        Name = "TestTask",
                        Description = "TestDescription",
                        DeadlineTime = new DateTime(2024, 9, 21)
                    },
                    new Task
                    {
                        Id = -2,
                        Name = "Create Table",
                        Description = "Create new Table Task",
                        DeadlineTime = new DateTime(2024, 9, 19)
                    },
                    new Task
                    {
                        Id = -3,
                        Name = "Make Coffee",
                        Description = "Make my favorite coffee ",
                        DeadlineTime = new DateTime(2024, 9, 5)
                    }
                    );
            });
        }
    }
}
