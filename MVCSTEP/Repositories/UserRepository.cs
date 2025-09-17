using Microsoft.EntityFrameworkCore;
using MVCSTEP.Data;
using MVCSTEP.Interfaces;
using MVCSTEP.Models;

namespace MVCSTEP.Repositories;

public class UserRepository : IUser
{
    private readonly ApplicationContext _context;
    public UserRepository(ApplicationContext context)
    {
        _context = context;
    }
 
    public async Task<User> CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }
 
    public async Task<bool> DeleteUserAsync(int id)
    {
        _context.Users.Remove(new User { Id = id });
        int rows = await _context.SaveChangesAsync();
        return rows > 0;
    }
 
    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }
 
    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(e => e.Id == id);
    }
 
    public async Task<bool> UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        int rows = await _context.SaveChangesAsync();
        return rows > 0;
    }
}