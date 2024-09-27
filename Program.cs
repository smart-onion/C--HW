using HW7;

internal class Program
{
    private static void Main(string[] args)
    {
        using (ApplicationContext db = new())
        {
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            var clients = new List<Client>
            {
                new Client{Name = "Alex", Email = "test@test.t", Address = "new street 11"},
                new Client{Name = "Bob", Email = "copy@test.t", Address = "old town 21"},
                new Client{Name = "Michal", Email = "michal1@test.t", Address = "old town 321"},

            };

            var products = new List<Product>
            {
                new Product { Name = "Apple", Price = 10.99 },
                new Product { Name = "Meet", Price = 5.99 },
                new Product { Name = "eggs", Price = 7.99 },
                new Product { Name = "water", Price = 9.99 },
            };
            var orders = new List<Order>
            {
                new Order { ClientId = 1, OrderDate = DateTime.Now,  Address = "Some address 1"},
                new Order { ClientId = 1, OrderDate = DateTime.Now, Address = "Some address 1"},
                new Order { ClientId = 2, OrderDate = DateTime.Now , Address = "Some address 1"},
            };
            var orderDetails = new List<OrderDetails>
            {
                new OrderDetails {OrderId = 1, ProductId = 1},
                new OrderDetails {OrderId = 1, ProductId = 2},
                new OrderDetails {OrderId = 1, ProductId = 3},
                new OrderDetails {OrderId = 2, ProductId = 3},
                new OrderDetails {OrderId = 2, ProductId = 2},
                new OrderDetails {OrderId = 2, ProductId = 1},
            };

            db.Clients.AddRange(clients);
            db.Products.AddRange(products);
            db.Orders.AddRange(orders);
            db.SaveChanges();

            db.OrderDetails.AddRange(orderDetails);
            db.SaveChanges();

            var query = db.Clients
                .Select(client => new
                {
                    ClientName = client.Name,
                    Email = client.Email,
                    Address = client.Address,
                    TotalOrders = client.Orders.Count(),
                    TotalSpent = client.Orders
                        .SelectMany(o => o.OrderDetails)
                        .Sum(od => od.Product.Price),
                    MostExpensiveProductName = client.Orders
                        .SelectMany(o => o.OrderDetails)
                        .OrderByDescending(od => od.Product.Price)
                        .Select(od => od.Product.Name)
                        .FirstOrDefault()
                }).ToList();

        }
    }
}