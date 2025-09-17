using MediatR;
using MVCSTEP.Models;

namespace MVCSTEP.Commands;

public class UpdateUserCommand : IRequest<User>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
 
    public UpdateUserCommand(int id, string name, string email)
    {
        Id = id;
        Name = name;
        Email = email;
    }
}