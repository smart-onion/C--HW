using MediatR;
using MVCSTEP.Application.Commands.ProductCommands;
using MVCSTEP.Core.Entities;
using MVCSTEP.Core.Interfaces;

namespace MVCSTEP.Application.Handlers;

public class GetProductByIdCommandHandler: IRequestHandler<GetProductByIdCommand, Product>
{
    private readonly IProduct _product;

    public GetProductByIdCommandHandler(IProduct product)
    {
        _product = product;
    }
    
    public async Task<Product> Handle(GetProductByIdCommand request, CancellationToken cancellationToken)
    {
        return await _product.GetByIdAsync(request.Id);
    }
}