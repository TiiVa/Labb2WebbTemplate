using Labb2Webb.Client.Services;
using Labb2Webb.Shared.DTOs.Customer;
using Labb2Webb.Shared.DTOs.Order;
using Labb2Webb.Shared.DTOs.Product;
using Labb2Webb.Shared.Interfaces;
using Microsoft.AspNetCore.Components;

namespace Labb2Webb.Client.Components.Pages;

public partial class ProductView : ComponentBase
{

	[Inject] private IProductService<ProductDto> ProductService { get; set; }
	[Inject] private ICustomerOrderService<PostOrderDto> CustomerOrderService { get; set; }
	[Inject] private ICustomerService<CustomerDto> CustomerService { get; set; }
	private List<ProductDto> Products { get; set; } = new();
	private List<CustomerDto> Customers { get; set; } = new();
	private CustomerDto CustomerForOrder { get; set; } = new();
	private PostOrderDto NewOrder { get; set; } = new();
	private List<ProductDto> CartProductsForOrder { get; set; } = new(); 
	private ProductDto ProductById { get; set; } = new();
	private int PreviousProductId { get; set; } = 0;
	private bool ProductExists { get; set; }
	private bool ShowAllProducts { get; set; }
	private bool GetSingleProduct { get; set; }
	private bool ShowCart { get; set; }
	private bool ShowSearchById { get; set; }
	private bool modalVisible { get; set; }

	Task ToggleModal()
	{
		modalVisible = !modalVisible;

		return Task.CompletedTask;
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{

		if (firstRender)
		{
			ShowSearchById = true;
			ShowAllProducts = true;
			Products.AddRange(await ProductService.GetAllProducts());
			Customers.AddRange(await CustomerService.GetAllCustomers());
			StateHasChanged();
		}

		await base.OnAfterRenderAsync(firstRender);
	}


	private async Task GetById()
	{

		GetSingleProduct = true;
		ShowAllProducts = false;
		ShowCart = false;

		ProductById = await ProductService.GetProductById(ProductById.ProductId);

		if (ProductById.ProductId == 0)
		{
			ProductExists = false;
		}
		else
		{
			ProductExists = true;
		}
		PreviousProductId = ProductById.ProductId;
	}

	private async Task SeeAllProducts()
	{
		ShowSearchById = true;
		ShowAllProducts = true;
		GetSingleProduct = false;
		ShowCart = false;

	}

	private async Task AddToCart(int productToAdd)
	{
		var getProductToAdd = await ProductService.GetProductById(productToAdd);

		CartProductsForOrder.Add(getProductToAdd);



	}

	private async Task RemoveFromCart(int productToRemove)
	{
		var getProductToRemove = CartProductsForOrder.FirstOrDefault(p => p.ProductId == productToRemove);

		if (CartProductsForOrder.Contains(getProductToRemove))
		{
			CartProductsForOrder.Remove(getProductToRemove);
		}

	}

	private int CountQuantity(ProductDto productDto)
	{
		if (productDto is null)
		{
			return 0;
		}

		int totalQuantity = CartProductsForOrder.Count(p => p.ProductId == productDto.ProductId);

		return totalQuantity;
	}

	private async Task SeeShoppingCart()
	{
		ShowCart = true;
		ShowAllProducts = false;
		GetSingleProduct = false;
		ShowSearchById = false;
	}

	private async Task CloseShoppingCart()
	{
		ShowCart = false;
		ShowAllProducts = true;
		GetSingleProduct = false;
		ShowSearchById = true;


	}

	private double TotalPrice()
	{
		double totalPrice = 0;

		foreach (var product in CartProductsForOrder)
		{
			totalPrice += product.Price;
		}

		return totalPrice;

	}

	private void ClearCart()
	{
		CartProductsForOrder.Clear();
	}

	private async Task AddOrder()
	{
		var customer = await CustomerService.GetCustomerById(CustomerForOrder.CustomerId);
		var products = new List<ProductDto>();

		foreach (var productDto in CartProductsForOrder)
		{
			var product = await ProductService.GetProductById(productDto.ProductId);
			products.Add(product);
		}


		var newOrder = new PostOrderDto()
		{
			OrderDate = DateTime.Now,
			Customer = customer,
			OrderTotal = TotalPrice(),
			Products = products
		};

		await CustomerOrderService.AddCustomerOrder(newOrder);

		ClearCart();

		modalVisible = true;
		ShowCart = false;
		ShowAllProducts = false;
		GetSingleProduct = false;

	}
}
