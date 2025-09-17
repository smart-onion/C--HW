using MediatR;
using MVCSTEP.Application.Commands.ReviewCommands;
using MVCSTEP.Core.Entities;

namespace MVCSTEP.Application.Handlers.ReviewHandlers;

public class GetReviewByIdCommandHandler: IRequestHandler<GetReviewByIdCommand, Review>
{
    public Task<Review> Handle(GetReviewByIdCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}