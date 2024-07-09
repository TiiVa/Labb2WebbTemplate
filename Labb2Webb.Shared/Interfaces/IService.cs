namespace Labb2Webb.Shared.Interfaces;

public interface IService<T> where T : class
{
	Task AddAsync(T item);
	Task DeleteAsync(T id);
	Task <IEnumerable<T>> GetAllAsync();
	Task<T> GetByIdAsync(T id);
	Task<T> UpdateAsync(int id, T item);

}