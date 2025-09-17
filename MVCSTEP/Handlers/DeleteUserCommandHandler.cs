using MediatR;
using MVCSTEP.Commands;
using MVCSTEP.Interfaces;

namespace MVCSTEP.Handlers;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly IUser _userRepository;
 
    public DeleteUserCommandHandler(IUser userRepository)
    {
        _userRepository = userRepository;
    }
 
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var success = await _userRepository.DeleteUserAsync(request.Id);
        return success;
    }
}