using Microsoft.AspNetCore.Identity;
namespace MVCSTEP.Core.Entities;

public class User : IdentityUser
{
    public IEnumerable<Review> Reviews { get; set; }
}