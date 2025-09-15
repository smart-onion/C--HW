using MVCSTEP.Models;

namespace MVCSTEP.ViewModels;

public class UserViewModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public IEnumerable<string> Roles { get; set; }
    public IEnumerable<User> Friends { get; set; } 
}