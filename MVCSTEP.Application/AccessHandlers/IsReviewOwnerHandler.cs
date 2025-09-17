using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MVCSTEP.Application.AccessHandlers.Requirements;
using MVCSTEP.Application.Interfaces;
using MVCSTEP.Core.Entities;
using MVCSTEP.Core.Interfaces;

namespace MVCSTEP.Application.AccessHandlers;

public class IsReviewOwnerHandler : AuthorizationHandler<IsReviewOwnerRequirement, Review>
{
    private readonly IAccountService _userManager;

    public IsReviewOwnerHandler(IAccountService userManager)
    {
        _userManager = userManager;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        IsReviewOwnerRequirement requirement, Review resource)

    {
        var currentUser = await _userManager.GetUserAsync();
        if (currentUser == null)
        {
            return;
        }

        if (resource.UserId == currentUser.Id)
        {
            context.Succeed(requirement);
        }
    }
}