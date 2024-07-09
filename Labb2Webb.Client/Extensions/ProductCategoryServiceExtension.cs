using Labb2Webb.Client.Services;
using Labb2Webb.Shared.Entities;
using Labb2Webb.Shared.Interfaces;

namespace Labb2Webb.Client.Extensions;

public static class ProductCategoryServiceExtension
{
	public static IServiceCollection AddProductCategoryService(this IServiceCollection services)
	{
		services.AddScoped<IProductCategoryService<ProductCategory>, ProductCategoryService>();
		return services;
	}
}