using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVCSTEP.Application.Commands.ProductCommands;
using MVCSTEP.Application.DTOs;
using MVCSTEP.Core.Entities;
using MVCSTEP.WebAPI.Filter;
using MVCSTEP.WebAPI.Filter.Authorization;

namespace MVCSTEP.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ProductController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var product = await _mediator.Send(new GetProductByIdCommand() { Id = id });
        if (product is null) return NotFound();
        
        return Ok(GetProductDto(product));
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateProductCommand request)
    {
        var product = await _mediator.Send(request);
        return Ok(GetProductDto(product));
    }
    
    private ProductDto GetProductDto(Product product) => _mapper.Map<ProductDto>(product);
}