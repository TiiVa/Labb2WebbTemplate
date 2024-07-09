using Labb2Webb.Shared.Entities;
using Labb2Webb.Shared.Interfaces;

namespace Labb2Webb.Client.Services;

public class ProductCategoryService : IProductCategoryService<ProductCategory>
{
	private readonly HttpClient _httpClient;

	public ProductCategoryService(IHttpClientFactory factory)
	{
		_httpClient = factory.CreateClient("ecommerceApi");
	}

	public async Task AddProductCategory(ProductCategory newCategory)
	{
		var response = await _httpClient.PostAsJsonAsync("/productCategories", newCategory);

		if (!response.IsSuccessStatusCode)
		{
			return;
		}
	}

	public async Task<IEnumerable<ProductCategory>> GetAllProductCategories()
	{
		var response = await _httpClient.GetAsync("/productCategories");

		if (!response.IsSuccessStatusCode)
		{
			return Enumerable.Empty<ProductCategory>();
		}

		var result = await response.Content.ReadFromJsonAsync<List<ProductCategory>>();
		return result ?? Enumerable.Empty<ProductCategory>();
	}

	public async Task<ProductCategory> GetProductCategoryById(int id)
	{
		var response = await _httpClient.GetAsync($"/productCategories/{id}");

		if (!response.IsSuccessStatusCode)
		{
			return new ProductCategory();
		}

		var result = await response.Content.ReadFromJsonAsync<ProductCategory>();

		return result;
	}
}