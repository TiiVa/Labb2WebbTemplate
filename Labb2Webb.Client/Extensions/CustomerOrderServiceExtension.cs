using Labb2Webb.Client.Services;
using Labb2Webb.Shared.DTOs.Order;
using Labb2Webb.Shared.Interfaces;

namespace Labb2Webb.Client.Extensions;

public static class CustomerOrderServiceExtension
{
	public static IServiceCollection AddCustomerOrderService(this IServiceCollection services)
	{
		services.AddScoped<ICustomerOrderService<PostOrderDto>, CustomerOrderService>();
		return services;
	}

}