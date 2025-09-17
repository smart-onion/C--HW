using MVCSTEP.Core.Entities;

namespace MVCSTEP.Core.Interfaces;

public interface IReview
{
    Task<Review> GetByIdAsync(int id);
    Task<IEnumerable<Review>> GetAllAsync();
    Task AddAsync(Review product);
    Task UpdateAsync(Review product);
    Task DeleteAsync(int id);
}