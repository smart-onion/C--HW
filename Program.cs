using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using HW2;
using System.Reflection;
using System.Runtime.InteropServices;
internal class Program
{
    private static void Main(string[] args)
    {
        var options = DbOptions<ApplicationContext>.GetOptions("application.json", "DefaultConnection");

        using (ApplicationContext db = new ApplicationContext())
        {
            /*            ShopManager.SetDbContext(db);

                        var products = new List<Product> {
                            new Product
                            {
                            Name = "Apple",
                            Category = "Fruits",
                            Price = 4.99
                            },
                            new Product
                            {
                                Name = "Potato",
                                Category = "Vegetables",
                                Price = 5.49
                            },
                            new Product
                            {
                                Name = "Water",
                                Category = "Beverage",
                                Price = 1.99
                            }
                        };

                        ShopManager.AddProducts(products);

                        ShopManager.AddOrder(new Order
                        {
                            Date = DateTime.Now,
                            Name = "Alex"
                        });

                        ShopManager.AddOrderLine(new OrderLine
                        {
                            OrderId = 1,
                            ProductId = 2
                        });*/

            /*var products = db.Products.ToList();
            ShopManager.ShowTable<Product>(products);*/

            PostgreTask();
        }
    }

   
    static void PostgreTask()
    {
        using (PostreSQLContext db = new PostreSQLContext())
        {
            db.Product.Add(new Product
            {
                Name = "Water",
                Category = "Beverage",
                Price = 1.99
            });
            db.SaveChanges();
            var products = db.Product.ToList();
            ShopManager.ShowTable<Product>(products);
        }
    }
    class PostreSQLContext : DbContext
    {
        public DbSet<Product> Product { get; set; }
        public PostreSQLContext() { }
        public PostreSQLContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost; Database=Shop; Username=postgres; Password=super");
        }
    }
}