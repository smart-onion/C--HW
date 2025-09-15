using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MVCSTEP.Models;

public class User : IdentityUser
{
    public List<User> Friends { get; set; } = new() ;
}