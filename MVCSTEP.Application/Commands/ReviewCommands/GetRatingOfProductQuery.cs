using MediatR;

namespace MVCSTEP.Application.Commands.ReviewCommands;

public record GetRatingOfProductQuery(int ProductId) : IRequest<decimal>
{
}