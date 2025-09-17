using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MVCSTEP.Application.Commands.UserCommands;
using MVCSTEP.Application.DTOs;
using MVCSTEP.Application.Interfaces;
using MVCSTEP.Core.Entities;
using MVCSTEP.Core.Interfaces;

namespace MVCSTEP.Application.Handlers.UserHandlers;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterDto>
{
    private readonly IAccountService _accountService;
    private readonly IMapper _mapper;

    public RegisterUserCommandHandler(IAccountService accountService, IMapper mapper)
    {
        _accountService = accountService;
        _mapper = mapper;
    }

    public async Task<RegisterDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);
        var result = await _accountService.CreateUserAsync(user, request.Password);
        return _mapper.Map<RegisterDto>(result);
    }
}