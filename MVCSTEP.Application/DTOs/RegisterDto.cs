namespace MVCSTEP.Application.DTOs;

public record RegisterDto
{
    public string Id { get; set; }
    public string Email { get; set; }
    public IEnumerable<string> Roles { get; set; }
}