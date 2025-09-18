using AutoMapper;
using MediatR;
using MVCSTEP.Application.Commands.ProductCommands;
using MVCSTEP.Application.Commands.ReviewCommands;
using MVCSTEP.Application.DTOs;
using MVCSTEP.Core.Entities;
using MVCSTEP.Core.Interfaces;

namespace MVCSTEP.Application.Handlers.ReviewHandlers;

public class UpdateReviewCommandHandler: IRequestHandler<UpdateReviewCommand, ReviewDto>
{
    private readonly IReview _review;
    private readonly IMapper _mapper;

    public UpdateReviewCommandHandler(IReview review,  IMapper mapper)
    {
        _review = review;
        _mapper = mapper;
    }
    
    public async Task<ReviewDto> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = _mapper.Map<Review>(request);
        await _review.UpdateAsync(review);
        return _mapper.Map<ReviewDto>(review);
    }
}