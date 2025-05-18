using AutoMapper;
using Core.DTOs.Product;
using Data.Context;
using Data.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Repository;

public class ProductRepository(VarojContext context, IMapper mapper, ILogger<ProductRepository> logger)
    : IProductRepository
{
    public async Task<List<ProductDto>> GetAllAsync()
    {
        logger.LogInformation($"Get all products");
        var products = await context.Products.ToListAsync();
        return mapper.Map<List<ProductDto>>(products);
    }

    public async Task<List<ProductDto>> GetAllByCategoryIdAsync(int categoryId)
    {
        logger.LogInformation($"Get all products by category : {categoryId}");
        var products = await context.Products
            .Where(p => p.Id == categoryId)
            .ToListAsync();
        return mapper.Map<List<ProductDto>>(products);
    }

    public async Task<ProductDto> GetByIdAsync(int id)
    {
        logger.LogInformation($"Get product by category : {id}");
        var product = await context.Products.SingleOrDefaultAsync(o => o.Id == id);
        return mapper.Map<ProductDto>(product);
    }

    public async Task<ProductDto> CreateAsync(AddProductDto product)
    {
        logger.LogInformation("Create product");
        var newproduct = mapper.Map<ProductDto>(product);
        await context.AddAsync(newproduct);
        await context.SaveChangesAsync();
        logger.LogInformation($"product created and saved to database");
        return mapper.Map<ProductDto>(newproduct);
    }

    public async Task<ProductDto> UpdateAsync(UpdateProductDto product, int id)
    {
        logger.LogInformation($"Update product by category : {id} : {product.Id}");
        var updateproduct = await context.Products.SingleOrDefaultAsync(o => o.Id == id) ??
                            throw new Exception("Product not found");
        context.Update(updateproduct);
        mapper.Map(product, updateproduct);
        await context.SaveChangesAsync();
        logger.LogInformation($"product updated and saved to database");
        return mapper.Map<ProductDto>(updateproduct);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = context.Products.SingleOrDefault(o => o.Id == id)
                      ?? throw new Exception("Product not found");
        context.Remove(product);
        await context.SaveChangesAsync();
        logger.LogInformation($"product deleted and saved to database");
        return true;
    }
}
