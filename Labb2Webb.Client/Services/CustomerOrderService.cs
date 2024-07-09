using Labb2Webb.Shared.DTOs.Order;
using Labb2Webb.Shared.Entities;
using Labb2Webb.Shared.Interfaces;

namespace Labb2Webb.Client.Services;

public class CustomerOrderService : ICustomerOrderService<PostOrderDto>
{
	private readonly HttpClient _httpClient;

	public CustomerOrderService(IHttpClientFactory factory)
	{
		_httpClient = factory.CreateClient("ecommerceApi");
	}

	public async Task AddCustomerOrder(PostOrderDto newOrder)
	{
		var response = await _httpClient.PostAsJsonAsync("/customerOrders/", newOrder);

		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}

	public async Task<IEnumerable<PostOrderDto>> GetAllCustomerOrders()
	{
		var response = await _httpClient.GetAsync("/customerOrders");

		if (!response.IsSuccessStatusCode)
		{
			return Enumerable.Empty<PostOrderDto>();
		}

		var result = await response.Content.ReadFromJsonAsync<List<PostOrderDto>>();
		return result ?? Enumerable.Empty<PostOrderDto>();
	}

	public async Task<IEnumerable<PostOrderDto>> GetAllCustomerOrdersForACustomer(int customerId)
	{
		var response = await _httpClient.GetAsync($"/customerOrders/{customerId}");
		if (!response.IsSuccessStatusCode)
		{
			return Enumerable.Empty<PostOrderDto>();
		}

		var result = await response.Content.ReadFromJsonAsync<List<PostOrderDto>>();
		return result ?? Enumerable.Empty<PostOrderDto>();
	}

	public async Task<PostOrderDto?> GetCustomerOrderById(int id)
	{
		var response = await _httpClient.GetAsync($"/customerOrders/{id}");

		if (!response.IsSuccessStatusCode)
		{
			return new PostOrderDto();
		}

		var result = await response.Content.ReadFromJsonAsync<PostOrderDto>();
		return result;
	}

	public async Task DeleteCustomerOrder(int id)
	{
		var response = await _httpClient.DeleteAsync($"/customerOrders/{id}");

		if (!response.IsSuccessStatusCode)
		{
			return;
		}

	}
}