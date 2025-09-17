using Microsoft.AspNetCore.Identity;

namespace MVCSTEP.Application.DTOs;

public record LoginDto
{
    public SignInResult SignInResult { get; set; }
    public string? Token { get; set; }
    public string? ErrorMessage { get; set; }
}