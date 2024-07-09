using System.Diagnostics.CodeAnalysis;
using Labb2Webb.Shared.DTOs.Product;
using Labb2Webb.Shared.Entities;
using Labb2Webb.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Labb2Webb.DataAccess.Repositories;

public class ProductRepository(ECommerceDbContext context) : IProductService<ProductDto>
{

	public async Task AddProduct(ProductDto newProduct)
	{
		var existingProduct = await context.Products
			.Include(p => p.ProductCategory)
			.FirstOrDefaultAsync(p => p.ProductId == newProduct.ProductId);

		if (existingProduct is not null)
		{
			return;
		}

		var productEntity = new Product()
		{
			Name = newProduct.Name,
			Description = newProduct.Description,
			Price = newProduct.Price,
			OnStock = newProduct.OnStock,
			InAssortment = newProduct.InAssortment,
			ProductCategory = newProduct.ProductCategory
		};

		await context.Products.AddAsync(productEntity);
		await context.SaveChangesAsync();
	}

	public async Task<IEnumerable<ProductDto>> GetAllProducts()
	{
		var products = context.Products.Include(p => p.ProductCategory);

		return products.Select(p => new ProductDto()
		{
			ProductId = p.ProductId,
			Name = p.Name,
			Description = p.Description,
			Price = p.Price,
			OnStock = p.OnStock,
			InAssortment = p.InAssortment,
			ProductCategory = p.ProductCategory
		});

	}


	public async Task<ProductDto> GetProductById(int id)
	{
		var product = await context.Products
			.Include(p => p.ProductCategory)
			.FirstOrDefaultAsync(p => p.ProductId == id);

		if (product is null)
		{
			return new ProductDto();
		}

		var productToReturn = new ProductDto()
		{
			ProductId = product.ProductId,
			Name = product.Name,
			Description = product.Description,
			Price = product.Price,
			OnStock = product.OnStock,
			InAssortment = product.InAssortment,
			ProductCategory = product.ProductCategory
		};

		return productToReturn;

	}

	public async Task UpdateProduct(int productId, ProductDto updatedProduct)
	{
		var product = await context.Products.FindAsync(productId);

		if (product is null)
		{
			return;
		}

		product.Name = updatedProduct.Name;
		product.Description = updatedProduct.Description;
		product.Price = updatedProduct.Price;
		product.OnStock = updatedProduct.OnStock;
		product.InAssortment = updatedProduct.InAssortment;
		product.ProductCategory = updatedProduct.ProductCategory;

		await context.SaveChangesAsync();
	}


	public async Task RemoveProduct(int id)
	{
		var product = await context.Products.FindAsync(id);

		context.Products.Remove(product);
		await context.SaveChangesAsync();
	}



}
