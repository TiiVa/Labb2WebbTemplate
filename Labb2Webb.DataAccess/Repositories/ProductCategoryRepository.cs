using Labb2Webb.Shared.Entities;
using Labb2Webb.Shared.Interfaces;

namespace Labb2Webb.DataAccess.Repositories;

public class ProductCategoryRepository(ECommerceDbContext context) : IProductCategoryService<ProductCategory>
{

    public async Task AddProductCategory(ProductCategory newCategory)
    {
        await context.ProductCategories.AddAsync(newCategory);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<ProductCategory>> GetAllProductCategories()
    {
        return context.ProductCategories;
    }

    public async Task<ProductCategory> GetProductCategoryById(int id)
    {
        return await context.ProductCategories.FindAsync(id);
    }


}