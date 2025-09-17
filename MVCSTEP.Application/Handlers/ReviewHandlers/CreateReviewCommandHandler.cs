using MediatR;
using MVCSTEP.Application.Commands.ReviewCommands;
using MVCSTEP.Core.Entities;
using MVCSTEP.Core.Interfaces;

namespace MVCSTEP.Application.Handlers.ReviewHandlers;

public class CreateReviewCommandHandler: IRequestHandler<CreateReviewCommand, Review>
{
    private readonly IReview _review;

    public CreateReviewCommandHandler(IReview review)
    {
        _review = review;
    }
    
    public async Task<Review> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = new Review
        {
            Comment = request.Comment,
            UserId = request.UserId,
            ProductId = request.ProductId,
            Rating = request.Rating,
        };
        await _review.AddAsync(review);
        return review;
    }
}