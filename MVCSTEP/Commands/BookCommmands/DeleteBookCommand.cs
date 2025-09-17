using MediatR;
using MVCSTEP.Models;

namespace MVCSTEP.Commands.BookCommmands;

public record DeleteBookCommand: IRequest<bool>
{
    public int Id { get; set; }
}