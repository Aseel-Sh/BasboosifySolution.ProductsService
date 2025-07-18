using AutoMapper;
using Basboosify.BusinessLogicLayer.DTO;
using Basboosify.DataAccessLayer.Entities;

namespace Basboosify.BusinessLogicLayer.Mappers;

public class ProductUpdateRequestToProductMappingProfile : Profile
{
    public ProductUpdateRequestToProductMappingProfile()
    {
        CreateMap<ProductUpdateRequest, Product>()
            .ForMember(dest => dest.ProductID, opt => opt.MapFrom(src => src.ProductID))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.QuantityInStock, opt => opt.MapFrom(src => src.QuantityInStock));

    }
}
