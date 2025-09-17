using MediatR;
using Microsoft.AspNetCore.Mvc;
using MVCSTEP.Application.Commands.ProductCommands;
using MVCSTEP.Application.Commands.ReviewCommands;

namespace MVCSTEP.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IMediator _mediator;

    public ReviewController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var review = await _mediator.Send(new GetReviewByIdCommand() { Id = id });
        if (review is null) return NotFound();
        return Ok(review);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateReviewCommand request)
    {
        var review = await _mediator.Send(request);
        return Ok(review);
    }
}