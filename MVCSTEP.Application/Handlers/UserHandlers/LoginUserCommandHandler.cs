using MediatR;
using Microsoft.AspNetCore.Identity;
using MVCSTEP.Application.Commands.UserCommands;
using MVCSTEP.Application.DTOs;
using MVCSTEP.Application.Interfaces;
using MVCSTEP.Core.Entities;
using MVCSTEP.Core.Interfaces;

namespace MVCSTEP.Application.Handlers.UserHandlers;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginDto>
{
    private readonly IAccountService _userManager;

    public LoginUserCommandHandler(IAccountService userManager)
    {
        _userManager = userManager;
    }

    public async Task<LoginDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        LoginDto dto = new LoginDto();
        var user = await _userManager.GetUserByEmailAsync(request.Email!);

        if (user is null)
        {
            dto.SignInResult = SignInResult.Failed;
            return dto;
        }

        if (await _userManager.IsLockedOutAsync(user))
        {
            dto.SignInResult = SignInResult.LockedOut;
            return dto;
        }

        var signInResult = await _userManager.CheckPasswordAsync(user, request.Password!);
        if (signInResult)
        {
            dto.SignInResult = SignInResult.Success;
            dto.Token = await _userManager.GetAccessTokenAsync(user);
            return dto;
        }
        dto.SignInResult = SignInResult.Failed;
        return dto;
    }
}