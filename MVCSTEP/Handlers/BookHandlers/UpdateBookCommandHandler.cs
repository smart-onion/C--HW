using MediatR;
using MVCSTEP.Commands.BookCommmands;
using MVCSTEP.Interfaces;
using MVCSTEP.Models;

namespace MVCSTEP.Handlers.BookHandlers;

public class UpdateBookCommandHandler: IRequestHandler<UpdateBookCommand, Book>
{
    private readonly IBook _book;

    public UpdateBookCommandHandler(IBook book)
    {
        _book = book;
    }

    public async Task<Book> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        
        var book = await _book.GetBookAsync(request.Id);
        if (!book.IsLocked)
        {
            book.IsLocked = true;
            _book.UpdateBookAsync(book);
            book.IsLocked = false;
            return await _book.UpdateBookAsync(book);
        }
        return null;
    }
}