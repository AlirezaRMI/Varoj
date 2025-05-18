using AutoMapper;
using Core.DTOs.Product;
using Data.Entities;

namespace Data.MaperProfiler;

public class ProductProfiler : Profile
{
    public ProductProfiler()
    {
        CreateMap<ProductDto, Product>().ReverseMap();
        CreateMap<ProductDto, AddProductDto>().ReverseMap();
        CreateMap<ProductDto, UpdateProductDto>().ReverseMap();
    }
}