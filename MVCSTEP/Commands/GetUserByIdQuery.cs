using MediatR;
using MVCSTEP.Models;

namespace MVCSTEP.Commands;

public class GetUserByIdQuery : IRequest<User>
{
    public int Id { get; set; }
    public GetUserByIdQuery(int id)
    {
        Id = id;
    }
}