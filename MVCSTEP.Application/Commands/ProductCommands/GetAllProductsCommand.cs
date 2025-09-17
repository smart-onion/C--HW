using MediatR;
using MVCSTEP.Application.DTOs;
using MVCSTEP.Core.Entities;

namespace MVCSTEP.Application.Commands.ProductCommands;

public class GetAllProductsCommand: IRequest<IEnumerable<ProductDto>>
{
    
}