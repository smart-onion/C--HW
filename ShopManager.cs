using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HW2
{
    public static class ShopManager
    {
        private static ApplicationContext db;

        public static void SetDbContext(ApplicationContext context)
        {
            db = context;
        }

        
        public static Product? GetProductById(int id)
        {
            return db.Products.FirstOrDefault(p => p.Id == id);
        }

        public static Order? GetOrderByID(int id)
        {
            return db.Orders.FirstOrDefault(o => o.Id == id);
        }

        public static OrderLine? GetOrderLineById(int id)
        {
            return db.OrderLine.FirstOrDefault(o => o.Id == id);
        }
        
        
        public static void AddProduct(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
        }

        public static void AddProducts(List<Product> products)
        {
            db.Products.AddRange(products);
            db.SaveChanges();
        }

        public static void AddOrder(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
        }

        public static void AddOrderLine(OrderLine orderLine)
        {
            db.OrderLine.Add(orderLine);
            db.SaveChanges();
        }

        public static void RemoveOrderById(int id)
        {
            db.Orders.Remove(db.Orders.FirstOrDefault(e => e.Id == id));
            db.SaveChanges();
        }

        public static void RemoveProductById(int id)
        {
            db.Products.Remove(db.Products.FirstOrDefault(e => e.Id == id));
            db.SaveChanges();
        }

        public static void RemoveOrderLineById(int id)
        {
            db.OrderLine.Remove(db.OrderLine.FirstOrDefault(e => e.Id == id));
            db.SaveChanges();
        }

        public static void ShowTable<T>(List<T> list)
        {
            Type objType = typeof(T);
            foreach (var item in list)
            {
                foreach (var eq in objType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    Console.Write(eq.GetValue(item) + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
