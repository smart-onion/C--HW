using MVCSTEP.Core.Entities;

namespace MVCSTEP.Core.Interfaces;

public interface IProduct
{
    Task<Product> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetAllAsync();
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
}