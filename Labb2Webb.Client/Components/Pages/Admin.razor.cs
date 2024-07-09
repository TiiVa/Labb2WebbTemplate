using Labb2Webb.Shared.DTOs.Customer;
using Labb2Webb.Shared.DTOs.Order;
using Labb2Webb.Shared.DTOs.Product;
using Labb2Webb.Shared.Entities;
using Labb2Webb.Shared.Interfaces;
using Microsoft.AspNetCore.Components;


namespace Labb2Webb.Client.Components.Pages;

public partial class Admin : ComponentBase
{
	[Inject] private IProductService<ProductDto> ProductService { get; set; }
	[Inject] private ICustomerService<CustomerDto> CustomerService { get; set; }
	[Inject] private IProductCategoryService<ProductCategory> ProductCategoryService { get; set; }
	[Inject] private ICustomerOrderService<PostOrderDto> CustomerOrderService { get; set; }

	private List<ProductDto> Products { get; set; } = new();
	private List<CustomerDto> Customers { get; set; } = new();
	private List<ProductCategory> ProductCategories { get; set; } = new();
	private List<PostOrderDto> CustomerOrders { get; set; } = new();
	private List<PostOrderDto> CustomerOrdersForACustomer { get; set; } = new();
	private PostProductDto NewProduct { get; set; } = new();
	private ProductDto ProductToRemove { get; set; } = new();
	private PutProductDto ProductToUpdate { get; set; } = new();
	private CustomerDto CustomerByEmail { get; set; } = new();
	private bool ShowCustomers { get; set; }
	private bool ShowProducts { get; set; }
	private bool RemoveAProduct { get; set; }
	private bool UpdateAProduct { get; set; }
	private bool AddNewProduct { get; set; }
	private bool ShowSingleCustomer { get; set; }
	public bool ShowCustomerOrders { get; set; }
	private int ProductId { get; set; }
	private string CustomerByEmailInsteadOfId { get; set; }
	private bool isLoading;


	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		if (firstRender)
		{
			ShowCustomerOrders = true;
			CustomerOrders.AddRange(await CustomerOrderService.GetAllCustomerOrders());
			Products.AddRange(await ProductService.GetAllProducts());
			Customers.AddRange(await CustomerService.GetAllCustomers());
			ProductCategories.AddRange(await ProductCategoryService.GetAllProductCategories());
			StateHasChanged();
		}
		await base.OnAfterRenderAsync(firstRender);
	}

	

	private async Task ShowLoading()
	{
		isLoading = true;

		await Task.Delay(TimeSpan.FromSeconds(15));

		isLoading = false;
	}


	private void ShowAddProductForm()
	{
		AddNewProduct = true;
		ShowCustomers = false;
		ShowProducts = false;
		RemoveAProduct = false;
		UpdateAProduct = false;
		ShowSingleCustomer = false;
		ShowCustomerOrders = false;

	}

	private void ShowUpdateProductForm()
	{
		AddNewProduct = false;
		ShowCustomers = false;
		ShowProducts = false;
		RemoveAProduct = false;
		UpdateAProduct = true;
		ShowSingleCustomer = false;
		ShowCustomerOrders = false;

	}

	private async Task AddProduct()
	{
		var productCategory = ProductCategories.FirstOrDefault(x => x.Id == NewProduct.ProductCategoryId);

		var newProduct = new ProductDto
		{
			Name = NewProduct.Name,
			Description = NewProduct.Description,
			Price = NewProduct.Price,
			ProductCategory = productCategory,
			InAssortment = NewProduct.InAssortment,
			OnStock = NewProduct.OnStock
		};

		await ProductService.AddProduct(newProduct);
		Products.Clear();
		Products.AddRange(await ProductService.GetAllProducts());

		AddNewProduct = false;
		ShowCustomers = false;
		ShowProducts = true;
		RemoveAProduct = false;
		UpdateAProduct = false;
		ShowSingleCustomer = false;
		ShowCustomerOrders = false;
	}

	private void SeeAllCustomers()
	{

		ShowCustomers = true;
		ShowProducts = false;
		RemoveAProduct = false;
		UpdateAProduct = false;
		AddNewProduct = false;
		ShowSingleCustomer = false;
		ShowCustomerOrders = false;

	}

	private void SeeAllProducts()
	{
		ShowProducts = true;
		ShowCustomers = false;
		RemoveAProduct = false;
		UpdateAProduct = false;
		AddNewProduct = false;
		ShowSingleCustomer = false;
		ShowCustomerOrders = false;

	}

	private void ShowRemoveProductForm()
	{
		AddNewProduct = false;
		ShowCustomers = false;
		ShowProducts = false;
		RemoveAProduct = true;
		UpdateAProduct = false;
		ShowSingleCustomer = false;
		ShowCustomerOrders = false;
	}

	private async Task RemoveProduct()
	{
		var product = Products.FirstOrDefault(x => x.ProductId == ProductToRemove.ProductId);

		await ProductService.RemoveProduct(product.ProductId);
		Products.Clear();
		Products.AddRange(await ProductService.GetAllProducts());

		RemoveAProduct = false;
		ShowCustomers = false;
		ShowProducts = false;
		UpdateAProduct = false;
		AddNewProduct = false;
		ShowSingleCustomer = false;
		ShowCustomerOrders = false;
	}

	private async Task GetProductById()
	{

		var result = await ProductService.GetProductById(ProductId);


		ProductToUpdate = new PutProductDto()
		{
			ProductId = result.ProductId,
			Name = result.Name,
			Description = result.Description,
			Price = result.Price,
			OnStock = result.OnStock,
			InAssortment = result.InAssortment,
			ProductCategoryId = result.ProductCategory.Id

		};

		ProductToUpdate.ProductId = ProductId;

	}


	private async Task UpdateProduct()
	{
		var productCategory = ProductCategories.FirstOrDefault(x => x.Id == ProductToUpdate.ProductCategoryId);

		var product = await ProductService.GetProductById(ProductToUpdate.ProductId);

		var updatedProductDto = new ProductDto
		{
			ProductId = ProductToUpdate.ProductId,
			Name = ProductToUpdate.Name,
			Description = ProductToUpdate.Description,
			Price = ProductToUpdate.Price,
			ProductCategory = new ProductCategory
			{
				Id = productCategory.Id,
				CategoryName = productCategory.CategoryName
			},
			InAssortment = ProductToUpdate.InAssortment,
			OnStock = ProductToUpdate.OnStock
		};


		await ProductService.UpdateProduct(product.ProductId, updatedProductDto);

		Products.Clear();
		Products.AddRange(await ProductService.GetAllProducts());

		UpdateAProduct = false;
		ShowCustomers = false;
		ShowProducts = true;
		RemoveAProduct = false;
		AddNewProduct = false;
		ShowSingleCustomer = false;
		ShowCustomerOrders = false;
	}

	private async Task GetCustomerByEmail()
	{
		var customerByEmail = await CustomerService.GetCustomerByEmail(CustomerByEmailInsteadOfId);

		CustomerByEmail = new CustomerDto
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


		ShowSingleCustomer = true;
		ShowProducts = false;
		RemoveAProduct = false;
		UpdateAProduct = false;
		AddNewProduct = false;
		ShowCustomers = false;
		ShowCustomerOrders = false;

	}

	private async Task ShowAllCustomerOrders()
	{
		var customerOrders = await CustomerOrderService.GetAllCustomerOrders();
		CustomerOrders = customerOrders.ToList();

		ShowCustomerOrders = true;
		ShowCustomers = false;
		ShowProducts = false;
		RemoveAProduct = false;
		UpdateAProduct = false;
		AddNewProduct = false;
		ShowSingleCustomer = false;


	}

	private async Task GetAllOrdersForACustomer(int customerId)
	{
		var getCustomerOrdersForACustomer = await CustomerOrderService.GetAllCustomerOrdersForACustomer(customerId);

		CustomerOrdersForACustomer = getCustomerOrdersForACustomer.ToList();
	}
}




	





