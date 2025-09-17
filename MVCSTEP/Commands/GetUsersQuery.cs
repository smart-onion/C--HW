using MediatR;
using MVCSTEP.Models;

namespace MVCSTEP.Commands;

public class GetUsersQuery: IRequest<List<User>>
{
    
}