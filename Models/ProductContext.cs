using Microsoft.EntityFrameworkCore;

namespace MVCHW1.Models
{
    public class ProductContext: DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public ProductContext() { Database.EnsureCreated(); }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetValue<string>("ConnectionStrings:SqlServer"));
            base.OnConfiguring(optionsBuilder);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
