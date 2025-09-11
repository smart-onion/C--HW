using Microsoft.EntityFrameworkCore;
using MVCSTEP.Data;
using MVCSTEP.Interfaces;
using MVCSTEP.Models;

namespace MVCSTEP.Repositories;

public class CategoryRepository : ICategory
{
    private readonly ApplicationContext _context;
 
    public CategoryRepository(ApplicationContext context)
    {
        _context = context;
    }
 
    public async Task AddCategoryAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
    }
 
    public async Task DeleteCategoryAsync(Category category)
    {
        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }
 
    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }
 
    public async Task<Category> GetCategoryAsync(string id)
    {
        return await _context.Categories.FirstOrDefaultAsync(e => e.Id.ToString() == id);
    }
 
    public async Task UpdateCategoryAsync(Category category)
    {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
    }
}