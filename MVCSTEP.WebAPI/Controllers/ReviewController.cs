using MediatR;
using Microsoft.AspNetCore.Mvc;
using MVCSTEP.Application.Commands.ProductCommands;
using MVCSTEP.Application.Commands.ReviewCommands;
using MVCSTEP.Application.DTOs;
using MVCSTEP.WebAPI.Filter.Authorization;

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

    [HttpGet]
    [Route("rating/{productId}")]
    public async Task<IActionResult> GetRatingOfProduct(int productId)
    {
        var reviews = await _mediator.Send(new GetRatingOfProductQuery(productId));
        return Ok(reviews);
    }
    [HttpGet("product/{productId}")]
    public async Task<IActionResult> GetReviewsByProduct(int productId)
    {
        var reviews = await _mediator.Send(new  GetReviewsByProductQuery(productId));
        return Ok(reviews);
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

    [HttpPut]
    [TypeFilter<ReviewOwnerAuthorizationFilter>]
    public async Task<IActionResult> Put([FromBody] UpdateReviewCommand request)
    {
        var review = await _mediator.Send(request);
        return Ok(review);
    }

    [HttpDelete("{id}")]
    [TypeFilter<ReviewOwnerAuthorizationFilter>]
    public async Task<IActionResult> Delete(int id)
    {
        var review = await _mediator.Send(new DeleteReviewCommand(id));
        return Ok(review);
    }
}