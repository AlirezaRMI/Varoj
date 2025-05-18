using AutoMapper;
using Core.DTOs.Category;
using Data.Entities;

namespace Data.MaperProfiler;

public class CateguryProfiler : Profile
{
    public CateguryProfiler()
    {
        CreateMap<CategoryDto, Category>().ReverseMap();
        CreateMap<CategoryDto, AddCategoryDto>().ReverseMap();
        CreateMap<CategoryDto, UpdateCategoryDto>().ReverseMap();
    }
}