using AutoMapper;
using MediatR;
using MVCSTEP.Application.Commands.ReviewCommands;
using MVCSTEP.Application.DTOs;
using MVCSTEP.Core.Interfaces;

namespace MVCSTEP.Application.Handlers.ReviewHandlers;

public class GetReviewByProductQueryHandler: IRequestHandler<GetReviewsByProductQuery, IEnumerable<ReviewDto>>
{
    private readonly IReview _review;
    private readonly IMapper _mapper;

    public GetReviewByProductQueryHandler(IReview review,  IMapper mapper)
    {
        _review = review;
        _mapper = mapper;
    }


    public async Task<IEnumerable<ReviewDto>> Handle(GetReviewsByProductQuery request, CancellationToken cancellationToken)
    {
        var reviews = await _review.GetReviewsByProductAsync(request.ProductId);
        return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
    }
}