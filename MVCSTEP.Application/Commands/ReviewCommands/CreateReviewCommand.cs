using System.ComponentModel.DataAnnotations;
using MediatR;
using MVCSTEP.Application.DTOs;

namespace MVCSTEP.Application.Commands.ReviewCommands;

public class CreateReviewCommand: IRequest<ReviewDto>
{
    [Required]
    [MaxLength(512)]
    public string Comment { get; set; }
    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }
    
    public string UserId { get; set; }
    public int ProductId { get; set; }
}