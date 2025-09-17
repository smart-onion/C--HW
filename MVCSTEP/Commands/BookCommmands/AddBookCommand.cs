using MediatR;
using MVCSTEP.Models;

namespace MVCSTEP.Commands.BookCommmands;

public class AddBookCommand: IRequest<Book>
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
}