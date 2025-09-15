using System.ComponentModel.DataAnnotations;

namespace MVCSTEP.Models;

public class UserFriends
{
    [Key] public int Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public string FriendId { get; set; }
    public User Friend { get; set; }
    public bool IsAccepted { get; set; } = false;
}