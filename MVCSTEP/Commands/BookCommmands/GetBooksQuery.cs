using MediatR;
using MVCSTEP.Models;

namespace MVCSTEP.Commands.BookCommmands;

public class GetBooksQuery: IRequest<List<Book>>
{
    
}