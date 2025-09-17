using Microsoft.AspNetCore.Authorization;
using MVCSTEP.Application.AccessHandlers.Requirements;
using MVCSTEP.Application.Commands.ProductCommands;
using MVCSTEP.Application.Interfaces;
using MVCSTEP.Core.Entities;
using MVCSTEP.Core.Interfaces;

namespace MVCSTEP.Application.AccessHandlers;

public class IsProductOwnerHandler: AuthorizationHandler<IsProductOwnerRequirement, Product>
{
    private readonly IAccountService _userManager;

    public IsProductOwnerHandler(IAccountService userManager)
    {
        _userManager = userManager;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        IsProductOwnerRequirement requirement, Product resource)

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