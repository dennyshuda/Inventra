using AutoMapper;
using Inventra.DTOs.Product;
using Inventra.Models;

namespace Inventra.MappingProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductDto, Product>()
        .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<UpdateProductDto, Product>()
        .ForMember(dest => dest.Id, opt => opt.Ignore())
        .ForAllMembers(opts =>
        {
            opts.Condition((src, dest, srcMember) => srcMember != null);
        });
    }
}
