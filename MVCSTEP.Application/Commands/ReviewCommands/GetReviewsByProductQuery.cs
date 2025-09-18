using MediatR;
using MVCSTEP.Application.DTOs;

namespace MVCSTEP.Application.Commands.ReviewCommands;

public record GetReviewsByProductQuery(int ProductId): IRequest<IEnumerable<ReviewDto>>
{
    
}