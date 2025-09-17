using AutoMapper;
using MediatR;
using MVCSTEP.Application.Commands.ProductCommands;
using MVCSTEP.Application.DTOs;
using MVCSTEP.Application.Interfaces;
using MVCSTEP.Core.Entities;
using MVCSTEP.Core.Interfaces;

namespace MVCSTEP.Application.Handlers;

public class CreateProductCommandHandler: IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly IProduct _product;
    private readonly IMapper _mapper;
    private readonly IAccountService  _accountService;
    public CreateProductCommandHandler(IProduct product,  IAccountService accountService, IMapper mapper)
    {
        _product = product;
        _accountService = accountService;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
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
        return _mapper.Map<ProductDto>(product);
    }
}