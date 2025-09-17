using MediatR;
using MVCSTEP.Application.DTOs;

namespace MVCSTEP.Application.Commands.ProductCommands;

public class CreateProductCommand : IRequest<ProductDto>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}