namespace Labb2Webb.Shared.Entities;

public class Order
{
	public int Id { get; set; }
	public DateTime OrderDate { get; set; }
	public Customer Customer { get; set; }

    public List<Product> Products { get; set; } 
	public double OrderTotal { get; set; }
	
}