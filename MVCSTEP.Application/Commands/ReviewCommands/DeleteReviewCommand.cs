using MediatR;

namespace MVCSTEP.Application.Commands.ReviewCommands;

public record DeleteReviewCommand(int Id): IRequest<bool>
{
}