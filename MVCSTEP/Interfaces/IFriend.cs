using MVCSTEP.Models;

namespace MVCSTEP.Interfaces;

public interface IFriend
{
    public bool IsFriendOf(string userId, string friendId);
    public Task<bool> IsFriendOfAsync(string userId, string friendId);
    
    public Task SendFriendRequestAsync(string userId, string friendId);
    public Task AcceptFriendRequestAsync(string userId, string friendId);
    
    public Task<IEnumerable<User>> GetFriendsAsync(string userId);
    public Task<IEnumerable<User>> GetRequestsAsync(string userId);
}