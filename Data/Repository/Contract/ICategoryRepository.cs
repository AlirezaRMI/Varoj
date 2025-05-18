using Core.DTOs.Category;
using Data.Entities;

namespace Data.Repository.Contract;

public interface ICategoryRepository
{
    public Task<List<CategoryDto>> GetAllAsync();
    public Task<CategoryDto> GetByIdAsync(int id);
    public Task<CategoryDto> AddAsync(AddCategoryDto category);
    public Task<CategoryDto> UpdateAsync(UpdateCategoryDto category,int id);
    public Task DeleteAsync(int id);
}