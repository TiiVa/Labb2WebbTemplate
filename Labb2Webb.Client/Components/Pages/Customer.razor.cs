using Labb2Webb.Shared.DTOs.Customer;
using Labb2Webb.Shared.DTOs.Order;
using Labb2Webb.Shared.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Labb2Webb.Client.Components.Pages;

public partial class Customer : ComponentBase
{
	[Inject] 
	private ICustomerService<CustomerDto> CustomerService { get; set; }
	[Inject] 
	private ICustomerOrderService<PostOrderDto> CustomerOrderService { get; set; }

	private CustomerDto CustomerLoginDto { get; set; } = new();
	private List<CustomerDto> Customers { get; set; } = new();
	private List<PostOrderDto> CustomerOrders { get; set; } = new();
	private CustomerDto NewCustomer { get; set; } = new();
	private CustomerDto CustomerToUpdate { get; set; } = new();
	private int CustomerId { get; set; }
	private bool UpdateProfile { get; set; }
	private bool ShowRegisterAsNewCustomerForm { get; set; }
	private bool CustomerIsLoggedIn { get; set; }
	private bool LoginSucceededModalVisible { get; set; }
	private LoginModalState LoginModalVisible { get; set; } = LoginModalState.None;
	private enum LoginModalState
	{
		None,
		Success,
		Failure
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			Customers.AddRange(await CustomerService.GetAllCustomers());
			CustomerOrders.AddRange(await CustomerOrderService.GetAllCustomerOrders());
			StateHasChanged();
		}
		await base.OnAfterRenderAsync(firstRender);

	}

	Task ToggleLoginSuccessModal()
	{
		LoginSucceededModalVisible = !LoginSucceededModalVisible;

		return Task.CompletedTask;
	}

	private async Task RegisterNewCustomer()
	{
		await CustomerService.AddCustomer(NewCustomer);
		Customers.Clear();
		Customers.AddRange(await CustomerService.GetAllCustomers());
	}

	private void ShowUpdateProfileForm()
	{

		UpdateProfile = true;
		ShowRegisterAsNewCustomerForm = false;

	}

	private void SeeRegisterAsNewCustomerForm()
	{
		ShowRegisterAsNewCustomerForm = true;
		UpdateProfile = false;
	}
	private async Task GetCustomer()
    {
	    var result = await CustomerService.GetCustomerById(CustomerId);

	    CustomerToUpdate = new CustomerDto()
	    {
		    CustomerId = result.CustomerId,
		    FirstName = result.FirstName,
		    LastName = result.LastName,
		    Email = result.Email,
		    StreetAddress = result.StreetAddress,
		    PostalCode = result.PostalCode,
		    City = result.City,
		    Country = result.Country,
		    Phone = result.Phone

	    };

	    CustomerToUpdate.CustomerId = CustomerId;

    }


	private async Task UpdateCustomer()
	{
		var customer = await CustomerService.GetCustomerById(CustomerToUpdate.CustomerId);


		var updatedCustomerDto = new CustomerDto
		{
			CustomerId = CustomerToUpdate.CustomerId,
			FirstName = CustomerToUpdate.FirstName,
			LastName = CustomerToUpdate.LastName,
			Email = CustomerToUpdate.Email,
			StreetAddress = CustomerToUpdate.StreetAddress,
			PostalCode = CustomerToUpdate.PostalCode,
			City = CustomerToUpdate.City,
			Country = CustomerToUpdate.Country,
			Phone = CustomerToUpdate.Phone
		};

		await CustomerService.UpdateCustomer(customer.CustomerId, updatedCustomerDto);
		
	}

	private async Task DeleteUser()
	{
		var customerToDelete = await CustomerService.GetCustomerById(CustomerId);
		await CustomerService.DeleteCustomer(customerToDelete.CustomerId);
	}

	private async Task LoginAccount()
	{
		var customer = await CustomerService.GetCustomerByEmail(CustomerLoginDto.Email);

		if (customer is not null && customer.Email == CustomerLoginDto.Email)
		{
			CustomerIsLoggedIn = true;
			ToggleLoginModal(LoginModalState.Success);
			ShowUpdateProfileForm();
			CustomerLoginDto.CustomerId = customer.CustomerId;
			CustomerLoginDto.FirstName = customer.FirstName;
			CustomerLoginDto.Email = null;
			

		}
		else
		{
			CustomerIsLoggedIn = false;
			ToggleLoginModal(LoginModalState.Failure);
			SeeRegisterAsNewCustomerForm();
		}

		
	}

	private void ToggleLoginModal(LoginModalState state)
	{
		LoginModalVisible = state;
	}
}