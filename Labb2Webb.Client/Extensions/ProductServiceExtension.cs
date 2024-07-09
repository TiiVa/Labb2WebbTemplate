using Labb2Webb.Client.Services;
using Labb2Webb.Shared.DTOs.Product;
using Labb2Webb.Shared.Interfaces;

namespace Labb2Webb.Client.Extensions;

public static class ProductServiceExtension
{
	public static IServiceCollection AddProductService(this IServiceCollection services)
	{
		services.AddScoped<IProductService<ProductDto>, ProductService>();
		return services;
	}
}