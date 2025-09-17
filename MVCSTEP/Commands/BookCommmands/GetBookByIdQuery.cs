using MediatR;
using MVCSTEP.Models;

namespace MVCSTEP.Commands.BookCommmands;

public record GetBookByIdQuery: IRequest<Book>
{
    public int Id { get; set; }
}