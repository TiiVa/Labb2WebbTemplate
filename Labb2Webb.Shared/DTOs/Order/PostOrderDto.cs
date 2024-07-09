using Labb2Webb.Shared.DTOs.Customer;
using Labb2Webb.Shared.DTOs.Product;

namespace Labb2Webb.Shared.DTOs.Order;

public class PostOrderDto
{
	public int Id { get; set; }
	public DateTime OrderDate { get; set; }
	public CustomerDto Customer { get; set; }
	public List<ProductDto> Products { get; set; } = new();

	public double OrderTotal { get; set; }




}