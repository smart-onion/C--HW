using Microsoft.AspNetCore.Authorization;
using MVCSTEP.Application.Commands.ProductCommands;

namespace MVCSTEP.Application.AccessHandlers.Requirements;

public class IsProductOwnerRequirement: IAuthorizationRequirement
{
    
}