using AutoMapper;
using MVCSTEP.Application.Commands.ReviewCommands;
using MVCSTEP.Application.DTOs;
using MVCSTEP.Core.Entities;
using MVCSTEP.Infrastructure.MapperActions;

namespace MVCSTEP.Infrastructure.Profiles;

public class ReviewProfile: Profile
{
    public ReviewProfile()
    {
        CreateMap<Review, ReviewDto>().ReverseMap();
        CreateMap<Review, CreateReviewCommand>().ReverseMap();
        CreateMap<UpdateReviewCommand, Review>()
            .AfterMap<UpdateReviewMapperAction>();
    }
}