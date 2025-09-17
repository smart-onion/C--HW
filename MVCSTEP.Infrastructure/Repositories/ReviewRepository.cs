using Microsoft.EntityFrameworkCore;
using MVCSTEP.Core.Entities;
using MVCSTEP.Core.Interfaces;
using MVCSTEP.Infrastructure.Database_Context;

namespace MVCSTEP.Infrastructure.Repositories;

public class ReviewRepository : IReview
{
    private readonly ApplicationContext _context;

    public ReviewRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Review> GetByIdAsync(int id)
    {
        return await _context.Reviews.FindAsync(id);
    }

    public async Task<IEnumerable<Review>> GetAllAsync()
    {
        return await _context.Reviews.ToListAsync();
    }

    public async Task AddAsync(Review product)
    {
        await _context.Reviews.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Review product)
    {
        _context.Reviews.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review != null)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }
    }
}