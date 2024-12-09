using hw8.Models;

namespace hw8.Services
{
    public class ProductService
    {
        List<Product> products = new();
        public void Add(Product product) { products.Add(product); }
        public Product? GetProduct(int id) 
        { 
            var product = products.FirstOrDefault(p => p.Id == id);
            return product; 
        }
        public List<Product> GetProducts() { return products; }
        public void Update(Product product) 
        {
            var currProduct = products.FirstOrDefault(p => p.Id == product.Id);
            currProduct.Name = product.Name;
            currProduct.Description = product.Description;
            currProduct.Category = product.Category;
            currProduct.Price = product.Price;
        }
    }
}
