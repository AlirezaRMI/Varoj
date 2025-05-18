using AutoMapper;
using Core.DTOs.Category;
using Data.Context;
using Data.Entities;
using Data.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Repository;

public class CategoryRepository(VarojContext context, IMapper mapper,ILogger<CategoryRepository> logger) : ICategoryRepository
{
    public async Task<List<CategoryDto>> GetAllAsync()
    {
        logger.LogInformation("GetAllAsync");
        var categories = await context.Categories.ToListAsync();
        return mapper.Map<List<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> GetByIdAsync(int id)
    {
        logger.LogInformation("GetByIdAsync");
        Category category = await context.Categories
            .SingleOrDefaultAsync(o => o.Id == id) ?? throw new Exception("Category not found");
        return mapper.Map<CategoryDto>(category);
        
    }

    public async Task<CategoryDto> AddAsync(AddCategoryDto category)
    {
        logger.LogInformation("AddAsync");
        var newcategory = mapper.Map<Category>(category);
        await context.Categories.AddAsync(newcategory);
        await context.SaveChangesAsync();
        logger.LogInformation("newcategory added");
        return mapper.Map<CategoryDto>(newcategory);
    }

    public async Task<CategoryDto> UpdateAsync(UpdateCategoryDto category, int id)
    {
        logger.LogInformation("UpdateAsync");
        var updateCategory = await context.Categories.SingleOrDefaultAsync(o => o.Id == id) ??
                             throw new Exception("Category not found");
        mapper.Map(category, updateCategory);
        context.Update(category);
        await context.SaveChangesAsync();
        logger.LogInformation($"category by id : {id} is updated");
        return mapper.Map<CategoryDto>(updateCategory);
    }

    public async Task DeleteAsync(int id)
    {
        logger.LogInformation("DeleteAsync");
        var category = await context.Categories.SingleOrDefaultAsync(o => o.Id == id) ??
                       throw new Exception("Category not found");
        context.Categories.Remove(category);
        await context.SaveChangesAsync();
        logger.LogInformation($"category by id : {id} is deleted");
    }
}