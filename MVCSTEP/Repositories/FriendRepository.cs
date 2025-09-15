using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCSTEP.Data;
using MVCSTEP.Interfaces;
using MVCSTEP.Models;

namespace MVCSTEP.Repositories;

public class FriendRepository : IFriend
{
    private readonly ApplicationContext _context;
    private readonly UserManager<User> _userManager;


    public FriendRepository(ApplicationContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    public bool IsFriendOf(string userId, string friendId)
    {
        var user = _userManager.FindByIdAsync(userId).Result;
        var friend = _userManager.FindByIdAsync(friendId).Result;
        if (user == null || friend == null) return false;

        return user.Friends.Contains(friend);
    } 
    public async Task<bool> IsFriendOfAsync(string userId, string friendId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        var friend = await _userManager.FindByIdAsync(friendId);
        if (user == null || friend == null) return false;

        return user.Friends.Contains(friend);
    }

   

    public async Task SendFriendRequestAsync(string userId, string friendId)
    {
        _context.UserFriends.Add(new UserFriends
        {
            UserId = userId,
            FriendId = friendId
        });
        await _context.SaveChangesAsync();
    }

    public async Task AcceptFriendRequestAsync(string userId, string friendId)
    {
        var request = await _context.UserFriends
            .FirstAsync(u => u.FriendId == userId && u.UserId == friendId);
        _context.UserFriends.Add(new UserFriends
        {
            UserId = userId,
            FriendId = friendId,
            IsAccepted = true
        });
        
        request.IsAccepted = true;
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetFriendsAsync(string userId)
    {
        return await _context.UserFriends
            .Where(uf => uf.UserId == userId && uf.IsAccepted)
            .Select(uf => uf.Friend)
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetRequestsAsync(string userId)
    {
        return await _context.UserFriends
            .Where(uf => uf.FriendId == userId && !uf.IsAccepted)
            .Select(uf => uf.User)
            .ToListAsync();
    }
}