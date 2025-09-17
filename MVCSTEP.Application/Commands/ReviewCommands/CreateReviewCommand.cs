using System.ComponentModel.DataAnnotations;
using MediatR;

namespace MVCSTEP.Application.Commands.ReviewCommands;

public class CreateReviewCommand: IRequest<Core.Entities.Review>
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