
namespace Labb2Webb.Shared.Interfaces;

public interface IProductService<T> where T : class
{
	Task AddProduct(T newProduct);
	Task<IEnumerable<T>> GetAllProducts();
	Task<T> GetProductById(int id);
	Task UpdateProduct(int id, T entity);
	
	Task RemoveProduct(int id);
}