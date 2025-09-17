using MediatR;
using MVCSTEP.Application.Commands.ProductCommands;
using MVCSTEP.Application.Interfaces;
using MVCSTEP.Core.Entities;
using MVCSTEP.Core.Interfaces;

namespace MVCSTEP.Application.Handlers;

public class CreateProductCommandHandler: IRequestHandler<CreateProductCommand, Product>
{
    private readonly IProduct _product;
    private readonly IAccountService  _accountService;
    public CreateProductCommandHandler(IProduct product,  IAccountService accountService)
    {
        _product = product;
        _accountService = accountService;
    }

    public async Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var user = await _accountService.GetUserAsync();
        if (user == null) return null;
        var product = new Product
        {
            Name = request.Name,
            Price = request.Price,
            UserId = user.Id
        };
        await _product.AddAsync(product);
        return product;
    }
}