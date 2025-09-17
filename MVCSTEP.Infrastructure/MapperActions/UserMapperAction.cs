using AutoMapper;
using MVCSTEP.Application.DTOs;
using MVCSTEP.Core.Entities;
using MVCSTEP.Core.Interfaces;

namespace MVCSTEP.Infrastructure.MapperActions;

public class UserMapperAction: IMappingAction<User, RegisterDto>
{
    private readonly IAccountService _accountService;

    public UserMapperAction(IAccountService accountService)
    {
        _accountService = accountService;
    }
    
    public void Process(User source, RegisterDto destination, ResolutionContext context)
    {
        var roles = _accountService.GetRolesAsync(_accountService.GetUserAsync().Result).Result;
        destination.Id = source.Id;
        destination.Email = source.Email;
        destination.Roles = roles;
    }
}