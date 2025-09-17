using System.Security.Claims;
using MVCSTEP.Core.Entities;

namespace MVCSTEP.Core.Interfaces;

public interface IAccountService
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByIdAsync(string id);
    Task<bool> IsLockedOutAsync(User user);
    Task<bool> CheckPasswordAsync(User user, string password);
    Task<string> GetAccessTokenAsync(User user);
    Task<User?> GetUserAsync();
    Task<User?> CreateUserAsync(User user, string password);
    Task<bool> AddToRoleAsync(User user, string role);
    Task<bool> AddToRolesAsync(User user, IEnumerable<string> roles);
    
    Task<IList<string>> GetRolesAsync(User user);
}