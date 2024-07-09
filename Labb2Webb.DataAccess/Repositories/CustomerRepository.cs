using System.Diagnostics.CodeAnalysis;
using Labb2Webb.Shared.DTOs.Customer;
using Labb2Webb.Shared.Entities;
using Labb2Webb.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Labb2Webb.DataAccess.Repositories;

public class CustomerRepository(ECommerceDbContext context) : ICustomerService<CustomerDto>
{

    public async Task AddCustomer(CustomerDto newCustomerDto)
    {
        var newCustomer = new Customer()
        {
            
            FirstName = newCustomerDto.FirstName,
            LastName = newCustomerDto.LastName, 
            Email = newCustomerDto.Email,
            Phone = newCustomerDto.Phone,
			StreetAddress = newCustomerDto.StreetAddress,
			PostalCode = newCustomerDto.PostalCode,
			City = newCustomerDto.City,
			Country = newCustomerDto.Country

		};
        await context.Customers.AddAsync(newCustomer);
        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CustomerDto>> GetAllCustomers()
    {
        var customers = await context.Customers.ToListAsync();

        return customers.Select(c => new CustomerDto()
        {
            CustomerId = c.CustomerId,
			FirstName = c.FirstName,
			LastName = c.LastName,
			Email = c.Email,
			Phone = c.Phone,
			StreetAddress = c.StreetAddress,
			PostalCode = c.PostalCode,
			City = c.City,
            Country = c.Country
		});
    }

    
    public async Task<CustomerDto> GetCustomerById(int id)
    {

        var customerById = await context.Customers.FindAsync(id);

        if (customerById is null)
        {
			return new CustomerDto();
		}

        var customerToReturn = new CustomerDto()
		{
			CustomerId = customerById.CustomerId,
			FirstName = customerById.FirstName,
			LastName = customerById.LastName,
			Email = customerById.Email,
			Phone = customerById.Phone,
			StreetAddress = customerById.StreetAddress,
			PostalCode = customerById.PostalCode,
			City = customerById.City,
			Country = customerById.Country
		};

        return customerToReturn;


	}

    public async Task<CustomerDto?> GetCustomerByEmail(string email)
    {
        var customerByEmail = await context.Customers.FirstOrDefaultAsync(c => c.Email == email);

		if (customerByEmail is null)
		{
			return new CustomerDto();
		}
		var customerToReturn = new CustomerDto()
        {
	        CustomerId = customerByEmail.CustomerId,
	        FirstName = customerByEmail.FirstName,
	        LastName = customerByEmail.LastName,
	        Email = customerByEmail.Email,
	        Phone = customerByEmail.Phone,
	        StreetAddress = customerByEmail.StreetAddress,
	        PostalCode = customerByEmail.PostalCode,
	        City = customerByEmail.City,
	        Country = customerByEmail.Country
        };
        return customerToReturn;
	}

    public async Task UpdateCustomer(int id, CustomerDto updatedCustomer)
    {
        var oldCustomer = await context.Customers.FindAsync(id);

        if (oldCustomer is null)
        {
            return;
        }

        oldCustomer.FirstName = updatedCustomer.FirstName;
        oldCustomer.LastName = updatedCustomer.LastName;
        oldCustomer.Email = updatedCustomer.Email;
        oldCustomer.StreetAddress = updatedCustomer.StreetAddress;
        oldCustomer.PostalCode = updatedCustomer.PostalCode;
        oldCustomer.City = updatedCustomer.City;
        oldCustomer.Country = updatedCustomer.Country;
        oldCustomer.Phone = updatedCustomer.Phone;


        await context.SaveChangesAsync();
    }

    public async Task DeleteCustomer(int id)
    {
        var customer = await context.Customers.FindAsync(id);

        if (customer is null)
        {
            return;
        }

        context.Customers.Remove(customer);
        await context.SaveChangesAsync();
    }
}