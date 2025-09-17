using MediatR;
using MVCSTEP.Commands.BookCommmands;
using MVCSTEP.Interfaces;
using MVCSTEP.Models;

namespace MVCSTEP.Handlers.BookHandlers;


public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<Book>>
{
    private readonly IBook _book;

    public GetBooksQueryHandler(IBook book)
    {
        _book = book;
    }

    public async Task<List<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        return await _book.GetBooksAsync();
    }
}