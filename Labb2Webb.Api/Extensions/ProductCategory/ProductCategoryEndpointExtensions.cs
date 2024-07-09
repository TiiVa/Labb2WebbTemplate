using Labb2Webb.Shared.Interfaces;

namespace Labb2Webb.Api.Extensions.ProductCategory;

public static class ProductCategoryEndpointExtensions
{
	public static IEndpointRouteBuilder MapProductCategoryEndpoints(this IEndpointRouteBuilder app)
	{
		var group = app.MapGroup("/productCategories");

		group.MapGet("/", GetProductCategories);

		group.MapPost("/", AddProductCategory);

		return app;
	}

	private static async Task<IResult> AddProductCategory(IProductCategoryService<Shared.Entities.ProductCategory> repo, Shared.Entities.ProductCategory category)
	{
		var existingCategory = await repo.GetProductCategoryById(category.Id);

		if (existingCategory is not null)
		{
			return Results.BadRequest($"Category with id {category.Id} already exists");
		}

		await repo.AddProductCategory(category);

		return Results.Ok();
	}

	private static async Task<IEnumerable<Shared.Entities.ProductCategory>> GetProductCategories(IProductCategoryService<Shared.Entities.ProductCategory> repo)
	{
		return await repo.GetAllProductCategories();
	}
}