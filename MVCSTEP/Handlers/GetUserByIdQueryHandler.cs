using MediatR;
using MVCSTEP.Commands;
using MVCSTEP.Interfaces;
using MVCSTEP.Models;

namespace MVCSTEP.Handlers;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
{
    private readonly IUser _userRepository;

    public GetUserByIdQueryHandler(IUser userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetUserByIdAsync(request.Id);
    }
}