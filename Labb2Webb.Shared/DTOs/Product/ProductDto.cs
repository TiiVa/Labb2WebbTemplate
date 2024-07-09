using Labb2Webb.Shared.Entities;

namespace Labb2Webb.Shared.DTOs.Product;

public class ProductDto
{
	public int ProductId { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public double Price { get; set; }
	public ProductCategory ProductCategory { get; set; }
	public bool OnStock { get; set; }
	public bool InAssortment { get; set; }

}