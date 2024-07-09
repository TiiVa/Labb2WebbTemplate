using Labb2Webb.Shared.DTOs.Customer;
using Labb2Webb.Shared.DTOs.Order;
using Labb2Webb.Shared.DTOs.Product;
using Labb2Webb.Shared.Entities;
using Labb2Webb.Shared.Interfaces;

namespace Labb2Webb.Api.Extensions.CustomerOrder;

public static class CustomerOrderEndpointExtensions
{
	public static IEndpointRouteBuilder MapCustomerOrderEndpoints(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("/customerOrders");

		group.MapGet("/", GetAllCustomerOrders);

		group.MapGet("/{customerId}", GetCustomerOrderById);

		group.MapPost("/", AddCustomerOrder);

		group.MapDelete("/{id}", DeleteCustomerOrder);

		return app;
	}

	private static async Task<IResult> DeleteCustomerOrder(ICustomerOrderService<PostOrderDto> repo, int id)
	{
		var orderToDelete = await repo.GetCustomerOrderById(id);

		if (orderToDelete is null)
		{
			return Results.NotFound($"Order with id {id} could not be found");
		}

		await repo.DeleteCustomerOrder(id);

		return Results.Ok();
	}

	private static async Task<IResult> AddCustomerOrder(ICustomerOrderService<PostOrderDto> customerOrderRepository, PostOrderDto orderIn, ICustomerService<CustomerDto> customerRepo, IProductService<ProductDto> productRepo)
	{
		var existingCustomer = await customerRepo.GetCustomerById(orderIn.Customer.CustomerId);

		if (existingCustomer is null)
		{
			return Results.NotFound($"Customer with id {orderIn.Customer.CustomerId} could not be found");
		}

		var products = new List<ProductDto>();

		foreach (var productId in orderIn.Products)
		{
			var product = await productRepo.GetProductById(productId.ProductId);

			if (product is null)
			{
				return Results.NotFound($"Product with id {productId} could not be found");
			}

			products.Add(product);
		}

		var orderTotal = products.Sum(p => p.Price);


		var newOrder = new PostOrderDto()
		{
			Customer = existingCustomer,
			Products = products,
			OrderTotal = orderTotal,
			OrderDate = DateTime.Now
		};

		await customerOrderRepository.AddCustomerOrder(newOrder);

		return Results.Ok();
	}

	private static async Task<IResult> GetCustomerOrderById(ICustomerOrderService<PostOrderDto> repo, int customerId, ICustomerService<CustomerDto> customerRepo)
	{
		var customer = await customerRepo.GetCustomerById(customerId);


		if (customer is null)
		{
			return Results.NotFound($"Customer with id {customerId} could not be found");
		}

		var customerOrders = await repo.GetAllCustomerOrdersForACustomer(customer.CustomerId);

		return Results.Ok(customerOrders);
	}

	private static async Task<IEnumerable<PostOrderDto>> GetAllCustomerOrders(ICustomerOrderService<PostOrderDto> repo)
	{
		return await repo.GetAllCustomerOrders();
	}
}