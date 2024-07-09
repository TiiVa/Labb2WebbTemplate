using Labb2Webb.Shared.DTOs.Customer;
using Labb2Webb.Shared.Interfaces;

namespace Labb2Webb.Api.Extensions.Customer;

public static class CustomerEndpointExtensions
{
	public static IEndpointRouteBuilder MapCustomerEndpoints(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("/customers");

		group.MapGet("/", GetAllCustomers);

		group.MapGet("/{id}", GetById);

		group.MapGet("/email/{email}", GetByEmail);

		group.MapPost("/", AddCustomer);

		group.MapPut("/{id}", UpdateCustomer);

		group.MapDelete("/{customerId}", DeleteCustomer);

		return app;
	}

	private static async Task<IResult> DeleteCustomer(ICustomerService<CustomerDto> repo, int customerId)
	{
		var existingCustomerToDelete = await repo.GetCustomerById(customerId);

		if (existingCustomerToDelete is null)
		{
			return Results.NotFound($"Customer with id {customerId} could not be found");
		}

		await repo.DeleteCustomer(customerId);

		return Results.Ok();
	}

	private static async Task<IResult> UpdateCustomer(ICustomerService<CustomerDto> repo, int id, CustomerDto customer)
	{
		var customerToUpdate = await repo.GetCustomerById(id);

		if (customerToUpdate is null)
		{
			return Results.NotFound($"Customer with id {id} could not be found");
		}

		await repo.UpdateCustomer(id, customer);

		return Results.Ok();
	}

	private static async Task<IResult> AddCustomer(ICustomerService<CustomerDto> repo, CustomerDto customer)
	{
		
		await repo.AddCustomer(customer);

		return Results.Ok();
	}

	private static async Task<IEnumerable<CustomerDto>> GetAllCustomers(ICustomerService<CustomerDto> repo)
	{
		return await repo.GetAllCustomers();
	}

	private static async Task<CustomerDto> GetById(ICustomerService<CustomerDto> repo, int id)
	{
		return await repo.GetCustomerById(id);
	}

	private static async Task<CustomerDto> GetByEmail(ICustomerService<CustomerDto> repo,
		string email)
	{
		return await repo.GetCustomerByEmail(email);
	}

}