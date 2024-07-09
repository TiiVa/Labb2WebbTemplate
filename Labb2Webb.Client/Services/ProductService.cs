using Labb2Webb.Shared.DTOs.Product;
using Labb2Webb.Shared.Interfaces;

namespace Labb2Webb.Client.Services;

public class ProductService : IProductService<ProductDto>
{
	private readonly HttpClient _httpClient;

	public ProductService(IHttpClientFactory factory)
	{
		_httpClient = factory.CreateClient("ecommerceApi");
	}
	public async Task AddProduct(ProductDto newProduct)
	{
		var response = await _httpClient.PostAsJsonAsync("/products", newProduct);

		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}

	public async Task<IEnumerable<ProductDto>> GetAllProducts()
	{
		var response = await _httpClient.GetAsync("/products");

		if (!response.IsSuccessStatusCode)
		{
			return Enumerable.Empty<ProductDto>();
		}

		var result = await response.Content.ReadFromJsonAsync<List<ProductDto>>();
		return result ?? Enumerable.Empty<ProductDto>();
	}

	public async Task<ProductDto> GetProductById(int id)
	{
		var response = await _httpClient.GetAsync($"/products/{id}");
		
		if (!response.IsSuccessStatusCode)
		{
			return new ProductDto();
		}

		var result = await response.Content.ReadFromJsonAsync<ProductDto>();
		return result;
	}

    public async Task UpdateProduct(int id, ProductDto entity)
    {
        var response = await _httpClient.PutAsJsonAsync($"products/{id}", entity);

        if (!response.IsSuccessStatusCode)
        {
            return;
        }
    }

    public async Task RemoveProduct(int id)
	{
		var response = await _httpClient.DeleteAsync($"/products/{id}");

		if (!response.IsSuccessStatusCode)
		{
			return;
		}

		
	}
}