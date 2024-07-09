using Labb2Webb.Shared.DTOs.Customer;
using Labb2Webb.Shared.Interfaces;

namespace Labb2Webb.Client.Services;

public class CustomerService : ICustomerService<CustomerDto>
{

	private readonly HttpClient _httpClient;

	public CustomerService(IHttpClientFactory factory)
	{
		_httpClient = factory.CreateClient("ecommerceApi");
	}
	public async Task AddCustomer(CustomerDto newCustomer)
	{
		var response = await _httpClient.PostAsJsonAsync("/customers", newCustomer);

		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}

	public async Task<IEnumerable<CustomerDto>> GetAllCustomers()
	{
		var response = await _httpClient.GetAsync("/customers");

		if (!response.IsSuccessStatusCode)
		{
			return Enumerable.Empty<CustomerDto>();
		}

		var result = await response.Content.ReadFromJsonAsync<List<CustomerDto>>();
		return result ?? Enumerable.Empty<CustomerDto>();
	}

	public async Task<CustomerDto> GetCustomerById(int id)
	{
		var response = await _httpClient.GetAsync($"/customers/{id}");

        if (!response.IsSuccessStatusCode)
        {
			return new CustomerDto();
        }
		var result = await response.Content.ReadFromJsonAsync<CustomerDto>();
		return result;
	}

	public async Task<CustomerDto?> GetCustomerByEmail(string email)
	{
		var response = await _httpClient.GetAsync($"/customers/email/{email}");

        if (!response.IsSuccessStatusCode)
        {
            return new CustomerDto();
        }

		var result = await response.Content.ReadFromJsonAsync<CustomerDto>();
		return result;
	}

    public async Task UpdateCustomer(int id, CustomerDto entity)
    {
        var response = await _httpClient.PutAsJsonAsync($"customers/{id}", entity);

        if (!response.IsSuccessStatusCode)
        {
            return;
        }
    }

 

	public async Task DeleteCustomer(int id)
	{
		var response = await _httpClient.DeleteAsync($"/customers/{id}");

		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}
}