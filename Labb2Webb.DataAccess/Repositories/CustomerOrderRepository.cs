using Labb2Webb.Shared.DTOs.Customer;
using Labb2Webb.Shared.DTOs.Order;
using Labb2Webb.Shared.DTOs.Product;
using Labb2Webb.Shared.Entities;
using Labb2Webb.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Labb2Webb.DataAccess.Repositories;

public class CustomerOrderRepository(ECommerceDbContext context) : ICustomerOrderService<PostOrderDto>
{
    public async Task AddCustomerOrder(PostOrderDto newOrderDto)
    {
		
		var customerEntity = await context.Customers
			.FirstOrDefaultAsync(c => c.CustomerId == newOrderDto.Customer.CustomerId);

		var products = new List<Product>();

		foreach (var productDto in newOrderDto.Products)
		{
			var product = await context.Products.FindAsync(productDto.ProductId);

			if (product is null)
			{
				return;
			}

			products.Add(product);
		}

		var orderTotal = products.Sum(p => p.Price);

		var newOrder = new Order()
	    {
		    OrderDate = newOrderDto.OrderDate,
		    Customer = customerEntity,
			OrderTotal = orderTotal,
			Products = products
			
		};

		
		await context.CustomerOrders.AddAsync(newOrder);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<PostOrderDto>> GetAllCustomerOrders()
    {
        var orders = await context.CustomerOrders
            .Include(c => c.Customer)
            .Include(p => p.Products)
            .ToListAsync();

        return orders.Select(o => new PostOrderDto
		{
			Id = o.Id,
            OrderDate = o.OrderDate,
            OrderTotal = o.OrderTotal,
			Customer = new CustomerDto
			{
				CustomerId = o.Customer.CustomerId,
				FirstName = o.Customer.FirstName,
				LastName = o.Customer.LastName,
				Email = o.Customer.Email,
				StreetAddress = o.Customer.StreetAddress,
				PostalCode = o.Customer.PostalCode,
				City = o.Customer.City,
				Country = o.Customer.Country,
				Phone = o.Customer.Phone
			},
			Products = o.Products.Select(p => new ProductDto
			{
				ProductId = p.ProductId,
				Name = p.Name,
				Description = p.Description,
				Price = p.Price
			}).ToList()
		});



	}

    public async Task<IEnumerable<PostOrderDto>> GetAllCustomerOrdersForACustomer(int customerId)
    {
        var customerOrders = await context.CustomerOrders
                .Where(c => c.Customer.CustomerId == customerId)
            .Include(p => p.Products)
			.ToListAsync();

        return customerOrders.Select(o => new PostOrderDto
        {
			Id = o.Id,
			OrderDate = o.OrderDate,
			OrderTotal = o.OrderTotal,
			Customer = new CustomerDto
			{
				CustomerId = o.Customer.CustomerId,
				FirstName = o.Customer.FirstName,
				LastName = o.Customer.LastName,
				Email = o.Customer.Email,
				StreetAddress = o.Customer.StreetAddress,
				PostalCode = o.Customer.PostalCode,
				City = o.Customer.City,
				Country = o.Customer.Country,
				Phone = o.Customer.Phone
			},
			Products = o.Products.Select(p => new ProductDto
			{
				ProductId = p.ProductId,
				Name = p.Name,
				Description = p.Description,
				Price = p.Price,
				InAssortment = p.InAssortment,
				OnStock = p.OnStock,
				ProductCategory = p.ProductCategory
			}).ToList()
		});
    }

    public async Task<PostOrderDto?> GetCustomerOrderById(int id)
    {
		var order = await context.CustomerOrders
			.Include(o => o.Customer)
			.ThenInclude(c => c.Orders)
			.Include(o => o.Products)
			.ThenInclude(p => p.ProductCategory)
			.FirstOrDefaultAsync(o => o.Id == id);

		return new PostOrderDto()
		{
			Id = order.Id,
			OrderDate = order.OrderDate,
			OrderTotal = order.OrderTotal,
			Customer = new CustomerDto
			{
				CustomerId = order.Customer.CustomerId,
				FirstName = order.Customer.FirstName,
				LastName = order.Customer.LastName,
				Email = order.Customer.Email,
				StreetAddress = order.Customer.StreetAddress,
				PostalCode = order.Customer.PostalCode,
				City = order.Customer.City,
				Country = order.Customer.Country,
				Phone = order.Customer.Phone
			},
			Products = order.Products.Select(p => new ProductDto
			{
				ProductId = p.ProductId,
				Name = p.Name,
				Description = p.Description,
				Price = p.Price,
				InAssortment = p.InAssortment,
				OnStock = p.OnStock,
				ProductCategory = p.ProductCategory
			}).ToList()
		};
    }

    public async Task DeleteCustomerOrder(int id)
    {
        var order = await context.CustomerOrders.FindAsync(id);

        if (order is null)
        {
            return;
        }

        context.CustomerOrders.Remove(order);
        await context.SaveChangesAsync();

    }
}