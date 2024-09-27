using Microsoft.EntityFrameworkCore;

namespace HW7
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Product> Products  { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

        public ApplicationContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=HW5;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetails>(c =>
            {
                c.HasOne(e=>e.Order).WithMany(e=>e.OrderDetails).HasForeignKey(e=>e.OrderId);
                c.HasOne(e=>e.Product).WithMany(e=>e.OrderDetails).HasForeignKey(e=>e.ProductId);
            });
            modelBuilder.Entity<Product>(e =>
            {
                e.HasMany(e => e.OrderDetails).WithOne(e => e.Product).HasForeignKey(e => e.ProductId);
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
