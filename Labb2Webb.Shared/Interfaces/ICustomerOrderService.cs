namespace Labb2Webb.Shared.Interfaces;

public interface ICustomerOrderService<T> where T : class
{
	Task AddCustomerOrder(T newOrder);
	Task<IEnumerable<T>> GetAllCustomerOrders();
	Task<IEnumerable<T>> GetAllCustomerOrdersForACustomer(int customerId);
	Task<T?> GetCustomerOrderById(int id);
	Task DeleteCustomerOrder(int id);
}