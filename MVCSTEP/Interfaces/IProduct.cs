using MVCSTEP.Models;

namespace MVCSTEP.Interfaces;

public interface IProduct
{
    IEnumerable<Product> GetAllProducts();
    void AddProduct(Product product);
    void UpdateProduct(Product product);
}