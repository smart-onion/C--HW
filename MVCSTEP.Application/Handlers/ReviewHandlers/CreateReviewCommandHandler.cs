using AutoMapper;
using MediatR;
using MVCSTEP.Application.Commands.ReviewCommands;
using MVCSTEP.Application.DTOs;
using MVCSTEP.Core.Entities;
using MVCSTEP.Core.Interfaces;

namespace MVCSTEP.Application.Handlers.ReviewHandlers;

public class CreateReviewCommandHandler: IRequestHandler<CreateReviewCommand, ReviewDto>
{
    private readonly IReview _review;
    private readonly IMapper _mapper;

    public CreateReviewCommandHandler(IReview review,  IMapper mapper)
    {
        _review = review;
        _mapper = mapper;
    }
    
    public async Task<ReviewDto> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = _mapper.Map<Review>(request);
        await _review.AddAsync(review);
        return _mapper.Map<ReviewDto>(review);
    }
}