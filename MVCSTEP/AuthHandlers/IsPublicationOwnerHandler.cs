using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MVCSTEP.AuthHandlers.Requirements;
using MVCSTEP.Models;

namespace MVCSTEP.AuthHandlers;

public class IsPublicationOwnerHandler: AuthorizationHandler<IsPublicationOwnerRequirement, Publication>
{
    private readonly UserManager<User> _userManager;

    public IsPublicationOwnerHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IsPublicationOwnerRequirement requirement,
        Publication resource)
    {
        var user = await _userManager.GetUserAsync(context.User);
        if (user == null) return;
        var isModerator = await _userManager.IsInRoleAsync(user, "Moderator"); 
        if (resource.UserId == user.Id || isModerator)
        {
            context.Succeed(requirement);
        }
    }
}