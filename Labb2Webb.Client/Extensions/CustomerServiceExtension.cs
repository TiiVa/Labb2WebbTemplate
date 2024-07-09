using Labb2Webb.Client.Services;
using Labb2Webb.Shared.DTOs.Customer;
using Labb2Webb.Shared.Interfaces;

namespace Labb2Webb.Client.Extensions;

public static class CustomerServiceExtension
{
	public static IServiceCollection AddCustomerService(this IServiceCollection services)
	{
		services.AddScoped<ICustomerService<CustomerDto>, CustomerService>();
		return services;
	}
}