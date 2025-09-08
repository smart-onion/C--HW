using Microsoft.AspNetCore.Identity;

namespace MVCSTEP.Models;

public class User: IdentityUser
{
    public int PulicationsCount { get; set; }
    public string? Name { get; set; }
}