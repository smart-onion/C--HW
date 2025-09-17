using Microsoft.EntityFrameworkCore;
using MVCSTEP.Data;
using MVCSTEP.Interfaces;
using MVCSTEP.Models;
using MVCSTEP.Repositories;

IConfigurationRoot _configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json").Build();
    
var services = new ServiceCollection()
    .AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("db"))
    .AddScoped<IProduct, ProductRepository>()
    .AddScoped<GetAllProductsQueryHandler>()
    .AddScoped<GetAllProductsQueryHandler>()
    .AddScoped<AddProductCommandHandler>();
    
using (ServiceProvider serviceProvider = services.BuildServiceProvider())
{
    var addProductHandler = serviceProvider.GetRequiredService<AddProductCommandHandler>();
    var getAllProductsHandler = serviceProvider.GetRequiredService<GetAllProductsQueryHandler>();
 
    // Добавляем продукты
    addProductHandler.Handle(new AddProductCommand { Name = "Apple", Price = 32 });
    addProductHandler.Handle(new AddProductCommand { Name = "Orange", Price = 56 });
    addProductHandler.Handle(new AddProductCommand { Name = "Cherry", Price = 70 });
 
    // Получаем список продуктов
    List<Product> products = getAllProductsHandler.Handle(new GetAllProductsQuery()).ToList();
}

// Команда для добавления нового продукта в каталог
public class AddProductCommand
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}
 
// Обработчик команды для добавления нового продукта в каталог
public class AddProductCommandHandler
{
    private readonly IProduct _products;
    public AddProductCommandHandler(IProduct products)
    {
        _products = products;
    }
 
    public void Handle(AddProductCommand command)
    {
        var product = new Product { Name = command.Name, Price = command.Price };
        _products.AddProduct(product);
    }
}
 
// Запрос для получения списка всех продуктов в каталоге
public class GetAllProductsQuery
{
}
 
// Обработчик запроса для получения списка всех продуктов в каталоге
public class GetAllProductsQueryHandler
{
    private readonly IProduct _products;
    public GetAllProductsQueryHandler(IProduct products)
    {
        _products = products;
    }
 
    public IEnumerable<Product> Handle(GetAllProductsQuery query)
    {
        return _products.GetAllProducts();
    }
}

public class UpdateProductCommand
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
public class UpdateProductCommandHandler
{
    private readonly IProduct _products;
    public UpdateProductCommandHandler(IProduct products)
    {
        _products = products;
    }
 
    public void Handle(UpdateProductCommand command)
    {
        Product product = new Product
        {
            Id = command.Id,
            Name = command.Name,
            Price = command.Price
        };
        _products.UpdateProduct(product);
    }
}