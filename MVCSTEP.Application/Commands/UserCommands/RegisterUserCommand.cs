using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MVCSTEP.Application.DTOs;
using MVCSTEP.Core.Entities;

namespace MVCSTEP.Application.Commands.UserCommands;

public class RegisterUserCommand: IRequest<IdentityResult>
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    [Required]
    [Compare("Password")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
    
}