using AutoMapper;
using MediatR;
using MVCSTEP.Application.Commands.ProductCommands;
using MVCSTEP.Application.DTOs;
using MVCSTEP.Core.Interfaces;

namespace MVCSTEP.Application.Handlers;

public class GetAllProductsCommandHandler : IRequestHandler<GetAllProductsCommand, IEnumerable<ProductDto>>
{
    private readonly IProduct _product;
    private readonly IMapper _mapper;

    public GetAllProductsCommandHandler(IProduct product, IMapper mapper)
    {
        _product = product;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsCommand request,
        CancellationToken cancellationToken)
    {
        var products = await _product.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }
}