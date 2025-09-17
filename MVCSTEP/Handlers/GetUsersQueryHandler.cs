using MediatR;
using MVCSTEP.Commands;
using MVCSTEP.Interfaces;
using MVCSTEP.Models;

namespace MVCSTEP.Handlers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<User>>
{
    private readonly IUser _users;
 
    public GetUsersQueryHandler(IUser users)
    {
        _users = users;
    }
 
 
    public async Task<List<User>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        return await _users.GetAllUsersAsync();
    }
}