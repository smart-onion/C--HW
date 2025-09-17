using MediatR;
using MVCSTEP.Commands.BookCommmands;
using MVCSTEP.Interfaces;
using MVCSTEP.Models;

namespace MVCSTEP.Handlers.BookHandlers;

public class AddBookCommandHandler : IRequestHandler<AddBookCommand, Book>
{
    private readonly IBook _book;

    public AddBookCommandHandler(IBook book)
    {
        _book = book;
    }
    
    public async Task<Book> Handle(AddBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Book()
        {
            Author = request.Author,
            Title = request.Title,
            Year = request.Year
        };
        return await _book.AddBookAsync(book);
    }
}