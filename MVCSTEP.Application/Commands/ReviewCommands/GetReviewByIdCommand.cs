using MediatR;

namespace MVCSTEP.Application.Commands.ReviewCommands;

public class GetReviewByIdCommand: IRequest<Core.Entities.Review>
{
    public int Id { get; set; }
}