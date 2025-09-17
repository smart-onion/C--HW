using AutoMapper;
using MediatR;
using MVCSTEP.Application.Commands.ProductCommands;
using MVCSTEP.Application.DTOs;
using MVCSTEP.Core.Entities;
using MVCSTEP.Core.Interfaces;

namespace MVCSTEP.Application.Handlers;

public class UpdateProductCommandHandler: IRequestHandler<UpdateProductCommand, ProductDto>
{
    private readonly IProduct _product;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IProduct product, IMapper mapper)
    {
        _product = product;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product =  _mapper.Map<Product>(request);
        await _product.UpdateAsync(product);
        return _mapper.Map<ProductDto>(product);
    }
}