using AutoMapper;
using MVCSTEP.Application.Commands.ProductCommands;
using MVCSTEP.Application.DTOs;
using MVCSTEP.Core.Entities;

namespace MVCSTEP.Infrastructure.Profiles;

public class ProductProfile: Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductCommand, Product>();
    }
}