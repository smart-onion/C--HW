using MediatR;
using MVCSTEP.Core.Entities;

namespace MVCSTEP.Application.Commands.ProductCommands;

public class GetProductByIdCommand: IRequest<Product>
{
    public int Id { get; set; }
}