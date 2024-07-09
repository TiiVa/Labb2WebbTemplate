namespace Labb2Webb.Shared.Interfaces;

public interface ICustomerService<T> where T : class
{
	Task AddCustomer(T newCustomer);
	Task<IEnumerable<T>> GetAllCustomers();
	Task<T> GetCustomerById(int id);
	Task<T?> GetCustomerByEmail(string email);
	Task UpdateCustomer(int id, T entity);
	Task DeleteCustomer(int id);
}