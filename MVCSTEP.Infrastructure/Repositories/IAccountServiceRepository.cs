using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using MVCSTEP.Application.Interfaces;
using MVCSTEP.Core.Entities;
using MVCSTEP.Core.Interfaces;

namespace MVCSTEP.Infrastructure.Repositories;

public class IAccountServiceRepository : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtService _jwtService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public IAccountServiceRepository(UserManager<User> userManager, IJwtService jwtService,  IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _httpContextAccessor = httpContextAccessor;
    }

    public Task<User?> GetUserByEmailAsync(string email) => _userManager.FindByEmailAsync(email);

    public Task<User?> GetUserByIdAsync(string id) => _userManager.FindByIdAsync(id);

    public Task<bool> IsLockedOutAsync(User user) => _userManager.IsLockedOutAsync(user);

    public Task<bool> CheckPasswordAsync(User user, string password) => _userManager.CheckPasswordAsync(user, password);
    public Task<string> GetAccessTokenAsync(User user) => Task.Run(() => _jwtService.CreateJwt(user));
    public Task<User?> GetUserAsync() =>  _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
    public async Task<IdentityResult> CreateUserAsync(User user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<bool> AddToRoleAsync(User user, string role)
    {
        var result = await _userManager.AddToRoleAsync(user, role);
        return result.Succeeded;
    }

    public async Task<bool> AddToRolesAsync(User user, IEnumerable<string> roles)
    {
        var result = await _userManager.AddToRolesAsync(user, roles);
        return result.Succeeded;
    }

    public Task<IList<string>> GetRolesAsync(User user) =>  _userManager.GetRolesAsync(user);
}