using MediatR;
using MVCSTEP.Commands;
using MVCSTEP.Interfaces;
using MVCSTEP.Models;

namespace MVCSTEP.Handlers;

public class AddUserCommandHandler: IRequestHandler<AddUserCommand, User>
{
    private readonly IUser _user;

    public AddUserCommandHandler(IUser user)
    {
        _user = user;
    }
    public async Task<User> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            Name = request.Name,
            Email = request.Email,
        };
        return await _user.CreateUserAsync(user);
    }
}