using AutoMapper;
using MediatR;
using MVCSTEP.Application.Commands.ProductCommands;
using MVCSTEP.Application.DTOs;
using MVCSTEP.Core.Entities;
using MVCSTEP.Core.Interfaces;

namespace MVCSTEP.Application.Handlers;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ProductDto>
{
    private readonly IProduct _product;
    private readonly IMapper _mapper;

    public DeleteProductCommandHandler(IProduct product, IMapper mapper)
    {
        _product = product;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = _product.GetByIdAsync(request.Id);
        if (product != null)
        {
            await _product.DeleteAsync(request.Id);
        }
        return _mapper.Map<ProductDto>(product);
    }
}