namespace Labb2Webb.Shared.DTOs.Product;

public class PostProductDto : ProductDto
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int ProductCategoryId { get; set; }
    public bool OnStock { get; set; }
    public bool InAssortment { get; set; }


}