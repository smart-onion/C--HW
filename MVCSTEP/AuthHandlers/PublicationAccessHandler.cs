using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCSTEP.AuthHandlers.Requirements;
using MVCSTEP.Models;

namespace MVCSTEP.AuthHandlers;

public class PublicationAccessHandler : AuthorizationHandler<PublicationAccessRequirement, Publication>
{
    private readonly UserManager<User> _userManager;

    public PublicationAccessHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PublicationAccessRequirement requirement,
        Publication resource)
    {
        
        var currentUser = await _userManager.GetUserAsync(context.User);
        if (currentUser == null)
        {
            return;
        }
        if (resource.UserId == currentUser.Id || currentUser.Friends.Any(u => u.Id == resource.UserId) ||
            resource.PublicationAccess == PublicationAccess.Public)
        {
            context.Succeed(requirement);
        }
    }
}