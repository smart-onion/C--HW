using AutoMapper;
using MVCSTEP.Application.Commands.UserCommands;
using MVCSTEP.Application.DTOs;
using MVCSTEP.Core.Entities;
using MVCSTEP.Core.Interfaces;

namespace MVCSTEP.Infrastructure.Profiles;

public class UserProfile : Profile
{
    public UserProfile(IAccountService accountService)
    {
        CreateMap<User, RegisterDto>()
            .ForMember(dest => dest.Roles,
                src => src.MapFrom(s => accountService.GetRolesAsync(s)));
        CreateMap<RegisterUserCommand, User>();
    }
}