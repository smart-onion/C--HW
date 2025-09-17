using AutoMapper;
using MediatR;
using MVCSTEP.Application.Commands.ReviewCommands;
using MVCSTEP.Application.DTOs;
using MVCSTEP.Core.Entities;
using MVCSTEP.Core.Interfaces;

namespace MVCSTEP.Application.Handlers.ReviewHandlers;

public class GetReviewByIdCommandHandler: IRequestHandler<GetReviewByIdCommand, ReviewDto>
{
    private readonly IReview _review;
    private readonly IMapper _mapper;

    public GetReviewByIdCommandHandler(IReview review,  IMapper mapper)
    {
        _review = review;
        _mapper = mapper;
    }
    public async Task<ReviewDto> Handle(GetReviewByIdCommand request, CancellationToken cancellationToken)
    {
        var product = await _review.GetByIdAsync(request.Id);
        return _mapper.Map<ReviewDto>(product);
    }
}