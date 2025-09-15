using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCSTEP.Data;
using MVCSTEP.Filters;
using MVCSTEP.Interfaces;
using MVCSTEP.Models;
using MVCSTEP.ViewModels;

namespace MVCSTEP.Repositories;

public class PublicationRepository : IPublication
{
    private readonly UserManager<User> _userManager;
    private readonly IFriend _friendRepository;
    private readonly ApplicationContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PublicationRepository(UserManager<User> userManager, IFriend friendRepository,
        ApplicationContext context, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _friendRepository = friendRepository;
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<Publication>?> GetPublications()
    {
        var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
        if (currentUser == null) return null;
        var friends = await _friendRepository.GetFriendsAsync(currentUser.Id);
        var friendIds = friends.Select(u => u.Id).ToList();
        
        var isModerator = await _userManager.IsInRoleAsync(currentUser, "Moderator");
        
        return await _context.Publication
            .Where(p => p.UserId == currentUser.Id
                        || friendIds.Contains(p.UserId) && p.PublicationAccess == PublicationAccess.FriendsOnly
                        || p.PublicationAccess == PublicationAccess.Public
                        || isModerator
                        )
            .Include(p => p.User)
            .ToListAsync();
    }
    [PublicationAccessAuthFilter("PublicationAccess")]
    public async Task<Publication?> GetPublication(int id)
    {
        return await _context.Publication.FirstOrDefaultAsync(p => p.Id == id);
    }

    public Task<Publication> UpdatePublication(Publication publication)
    {
        throw new NotImplementedException();
    }

    public Task<Publication> AddPublication(Publication publication)
    {
        throw new NotImplementedException();
    }

    public Task<Publication> DeletePublication(int id)
    {
        throw new NotImplementedException();
    }
}