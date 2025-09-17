using MediatR;
using MVCSTEP.Application.DTOs;

namespace MVCSTEP.Application.Commands.ProductCommands;

public class UpdateProductCommand: IRequest<ProductDto>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    
}