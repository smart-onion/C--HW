using AutoMapper;
using MVCSTEP.Application.Commands.ReviewCommands;
using MVCSTEP.Application.DTOs;
using MVCSTEP.Core.Entities;
using MVCSTEP.Core.Interfaces;

namespace MVCSTEP.Infrastructure.MapperActions;

public class UpdateReviewMapperAction:IMappingAction<UpdateReviewCommand, Review>
{
    private readonly IReview _review;
    public UpdateReviewMapperAction(IReview review)
    {
        _review = review;
    }

    public void Process(UpdateReviewCommand source, Review destination, ResolutionContext context)
    {
        var review = _review.GetByIdAsync(source.Id).Result;
        
        destination.Id = source.Id;
        destination.Comment = source.Comment;
        destination.Rating = source.Rating;
        destination.Updated =  DateTime.Now;
        destination.UserId = review.UserId;
        destination.ProductId = review.ProductId;
        
    }
}