using Labb2Webb.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace Labb2Webb.DataAccess;

public class ECommerceDbContext : DbContext
{
	public DbSet<Customer> Customers { get; set; }
	public DbSet<Product> Products { get; set; }
	public DbSet<ProductCategory> ProductCategories { get; set; }
	public DbSet<Order> CustomerOrders { get; set; }


	public ECommerceDbContext(DbContextOptions options) : base(options)
	{

	}
}