using MediatR;
using MVCSTEP.Commands.BookCommmands;
using MVCSTEP.Interfaces;
using MVCSTEP.Models;

namespace MVCSTEP.Handlers.BookHandlers;

public class GetBookByIdQueryHandler:  IRequestHandler<GetBookByIdQuery, Book>
{
    private readonly IBook _book;

    public GetBookByIdQueryHandler(IBook book)
    {
        _book = book;
    }

    public async Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        return await _book.GetBookAsync(request.Id);
    }
}