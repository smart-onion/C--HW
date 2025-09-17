using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MVCSTEP.Application.DTOs;

namespace MVCSTEP.Application.Commands.UserCommands;

public class LoginUserCommand: IRequest<LoginDto>
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    public bool RememberMe { get; set; }
    public string? ReturnUrl { get; set; }
}