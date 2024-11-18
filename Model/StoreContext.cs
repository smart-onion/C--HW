using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace hw4.Model
{
    public class StoreContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        public StoreContext() { Database.EnsureCreated(); }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetValue<string>("ConnectionStrings:SqlServer"));
            base.OnConfiguring(optionsBuilder);
        }
    }
}
