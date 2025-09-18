using AutoMapper;
using MediatR;
using MVCSTEP.Application.Commands.ReviewCommands;
using MVCSTEP.Core.Interfaces;

namespace MVCSTEP.Application.Handlers.ReviewHandlers;

public class DeleteReviewCommandHandler: IRequestHandler<DeleteReviewCommand, bool>
{
    private readonly IReview _review;
    private readonly IMapper _mapper;

    public DeleteReviewCommandHandler(IReview review,  IMapper mapper)
    {
        _review = review;
        _mapper = mapper;
    }
    
    public async Task<bool> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        await _review.DeleteAsync(request.Id);
        return true;
    }
}