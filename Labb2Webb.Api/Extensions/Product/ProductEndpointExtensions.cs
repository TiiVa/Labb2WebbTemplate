using Labb2Webb.Shared.DTOs.Product;
using Labb2Webb.Shared.Interfaces;


namespace Labb2Webb.Api.Extensions.Product;

public static class ProductEndpointExtensions
{
	public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("/products");

		group.MapGet("/", GetAllProducts);

		group.MapGet("/{id}", GetById);

		group.MapPost("/", AddProduct);

		group.MapPut("/{id}", UpdateProduct);

		group.MapDelete("/{productId}", DeleteProduct);

		return app;
	}

	private static async Task<IResult> DeleteProduct(IProductService<ProductDto> repo, int productId)
	{
		var existingProductToDelete = await repo.GetProductById(productId);

		if (existingProductToDelete is null)
		{
			return Results.NotFound($"Product with id {productId} could not be found");
		}

		await repo.RemoveProduct(productId);

		return Results.Ok();
	}

	private static async Task<IResult> UpdateProduct(IProductService<ProductDto> repo, int id, ProductDto prodIn, IProductCategoryService<Shared.Entities.ProductCategory> categoryRepo)
	{
		var productToUpdate = await repo.GetProductById(id);

		if (productToUpdate is null)
		{
			return Results.NotFound($"Product with id {id} could not be found");
		}

		var productCategory = await categoryRepo.GetProductCategoryById(prodIn.ProductCategory.Id);

		if (productCategory is null)
		{
			return Results.BadRequest($"Product category with id {prodIn.ProductCategory.Id} could not be found");
		}


		var newProduct = new ProductDto
		{
			ProductId = prodIn.ProductId,
			Name = prodIn.Name,
			Description = prodIn.Description,
			Price = prodIn.Price,
			OnStock = prodIn.OnStock,
			InAssortment = prodIn.InAssortment,
			ProductCategory = productCategory
		};


		await repo.UpdateProduct(id, newProduct);

		return Results.Ok();
	}

	private static async Task<IResult> AddProduct(IProductService<ProductDto> repo, ProductDto prodIn, IProductCategoryService<Shared.Entities.ProductCategory> categoryRepo)
	{
		
		var productCategory = await categoryRepo.GetProductCategoryById(prodIn.ProductCategory.Id);

		if (productCategory is null)
		{
			return Results.BadRequest($"Product category with id {prodIn.ProductCategory.Id} could not be found");
		}


		var newProduct = new ProductDto
		{
			ProductId = prodIn.ProductId,
			Name = prodIn.Name,
			Description = prodIn.Description,
			Price = prodIn.Price,
			OnStock = prodIn.OnStock,
			InAssortment = prodIn.InAssortment,
			ProductCategory = productCategory
		};

		await repo.AddProduct(newProduct);

		return Results.Ok();
	}

	private static async Task<IResult> GetById(IProductService<ProductDto> repo, int id)
	{
		var product = await repo.GetProductById(id);

		if (product is null)
		{
			return Results.NotFound($"The product with id {id} could not be found");
		}

		return Results.Ok(product);
	}

	private static async Task<IEnumerable<ProductDto>> GetAllProducts(IProductService<ProductDto> repo)
	{
		return await repo.GetAllProducts();
	}
}