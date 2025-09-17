using MediatR;
using MVCSTEP.Application.DTOs;

namespace MVCSTEP.Application.Commands.ProductCommands;

public class DeleteProductCommand: IRequest<ProductDto>
{
    public int Id { get; set; }
}