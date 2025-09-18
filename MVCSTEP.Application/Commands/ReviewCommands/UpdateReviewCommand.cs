using MediatR;
using MVCSTEP.Application.DTOs;

namespace MVCSTEP.Application.Commands.ReviewCommands;

public class UpdateReviewCommand: IRequest<ReviewDto>
{
    public int Id { get; set; }
    public string Comment { get; set; }
    public decimal Rating{get;set;}
}