namespace Labb2Webb.Shared.Interfaces;

public interface IProductCategoryService<T> where T : class
{
	Task AddProductCategory(T newCategory);
	Task<IEnumerable<T>> GetAllProductCategories();
	Task<T> GetProductCategoryById(int id);
}