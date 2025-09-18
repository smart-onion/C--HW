using AutoMapper;
using MediatR;
using MVCSTEP.Application.Commands.ReviewCommands;
using MVCSTEP.Core.Interfaces;

namespace MVCSTEP.Application.Handlers.ReviewHandlers;

public class GetRatingOfProductQueryHandler: IRequestHandler<GetRatingOfProductQuery, decimal>
{
    private readonly IReview _review;

    public GetRatingOfProductQueryHandler(IReview review)
    {
        _review = review;
    }
    
    public async Task<decimal> Handle(GetRatingOfProductQuery request, CancellationToken cancellationToken)
    {
        return await _review.GetRatingOfProduct(request.ProductId); 
    }
}