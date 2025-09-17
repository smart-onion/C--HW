using MediatR;
using Microsoft.AspNetCore.Mvc;
using MVCSTEP.Commands.BookCommmands;
using MVCSTEP.Interfaces;

namespace MVCSTEP.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BookController : Controller
{
    private readonly IMediator _mediator;

    public BookController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var book = await _mediator.Send(new GetBookByIdQuery { Id = id });
        return Ok(book);
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var book = await _mediator.Send(new GetBooksQuery());
        return Ok(book);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post(AddBookCommand command)
    {
        var book = await _mediator.Send(command);
        return Ok(book);
    }
    
    [HttpPut]
    public async Task<IActionResult> Put(UpdateBookCommand command)
    {
        var book = await _mediator.Send(command);
        return Ok(book);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _mediator.Send(new DeleteBookCommand { Id = id });
        return Ok(book);
    }

}