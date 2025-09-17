using AutoMapper;
using MVCSTEP.Application.Commands.UserCommands;
using MVCSTEP.Application.DTOs;
using MVCSTEP.Core.Entities;
using MVCSTEP.Core.Interfaces;
using MVCSTEP.Infrastructure.MapperActions;

namespace MVCSTEP.Infrastructure.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, RegisterDto>()
            .AfterMap<UserMapperAction>();
        CreateMap<RegisterUserCommand, User>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
    }
}