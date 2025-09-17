using MVCSTEP.Data;
using MVCSTEP.Interfaces;
using MVCSTEP.Models;

namespace MVCSTEP.Repositories;

public class ProductRepository : IProduct
{
    private readonly ApplicationContext _context;

    public ProductRepository(ApplicationContext context)
    {
        _context = context;
    }

    public IEnumerable<Product> GetAllProducts()
    {
        return _context.Products.ToList();
    }

    public void AddProduct(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public void UpdateProduct(Product product)
    {
        _context.Products.Update(product);
        _context.SaveChanges();
    }
}