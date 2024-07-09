using System.Text.Json.Serialization;

namespace Labb2Webb.Shared.Entities;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public bool OnStock { get; set; }
    public bool InAssortment { get; set; }
    public ProductCategory ProductCategory { get; set; }
    [JsonIgnore]
    public virtual ICollection<Order> CustomersOrders { get; set; }

}