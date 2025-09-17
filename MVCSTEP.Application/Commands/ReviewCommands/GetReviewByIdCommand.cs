using MediatR;
using MVCSTEP.Application.DTOs;

namespace MVCSTEP.Application.Commands.ReviewCommands;

public class GetReviewByIdCommand: IRequest<ReviewDto>
{
    public int Id { get; set; }
}