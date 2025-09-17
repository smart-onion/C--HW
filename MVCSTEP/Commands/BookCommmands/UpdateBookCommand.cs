using MediatR;
using MVCSTEP.Models;

namespace MVCSTEP.Commands.BookCommmands;

public class UpdateBookCommand: IRequest<Book>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
}