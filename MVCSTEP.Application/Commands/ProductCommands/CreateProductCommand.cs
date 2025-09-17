using MediatR;
using MVCSTEP.Core.Entities;

namespace MVCSTEP.Application.Commands.ProductCommands;

public class CreateProductCommand : IRequest<Product>
{
    public string Name { get; set; }
    public decimal Price { get; set; }
}