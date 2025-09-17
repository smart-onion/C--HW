using MediatR;
using MVCSTEP.Models;

namespace MVCSTEP.Commands;


public class AddUserCommand : IRequest<User>
{
    public string Name { get; set; }
    public string Email { get; set; }
 
    public AddUserCommand(string name, string email)
    {
        Name = name;
        Email = email;
    }
}