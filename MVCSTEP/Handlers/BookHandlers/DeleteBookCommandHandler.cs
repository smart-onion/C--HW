using MediatR;
using MVCSTEP.Commands.BookCommmands;
using MVCSTEP.Interfaces;
using MVCSTEP.Models;

namespace MVCSTEP.Handlers.BookHandlers;

public class DeleteBookCommandHandler:  IRequestHandler<DeleteBookCommand, bool>
{
    private readonly IBook _book;

    public DeleteBookCommandHandler(IBook book)
    {
        _book = book;
    }

    public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var result = await _book.DeleteBookAsync(request.Id);
        return  result;
    }
}