using Labb2Webb.Api.Extensions.Customer;
using Labb2Webb.Api.Extensions.CustomerOrder;
using Labb2Webb.Api.Extensions.Product;
using Labb2Webb.Api.Extensions.ProductCategory;
using Labb2Webb.DataAccess;
using Labb2Webb.DataAccess.Repositories;
using Labb2Webb.Shared.DTOs.Customer;
using Labb2Webb.Shared.DTOs.Order;
using Labb2Webb.Shared.DTOs.Product;
using Labb2Webb.Shared.Entities;
using Labb2Webb.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ECommerceDb");

builder.Services.AddDbContext<ECommerceDbContext>(
    options =>
        options.UseSqlServer(connectionString));


builder.Services.AddScoped<ICustomerService<CustomerDto>, CustomerRepository>();
builder.Services.AddScoped<IProductService<ProductDto>, ProductRepository>();
builder.Services.AddScoped<IProductCategoryService<ProductCategory>, ProductCategoryRepository>();
builder.Services.AddScoped<ICustomerOrderService<PostOrderDto>, CustomerOrderRepository>();

var app = builder.Build();

app.MapProductEndpoints();
app.MapProductCategoryEndpoints();
app.MapCustomerEndpoints();
app.MapCustomerOrderEndpoints();


app.Run();
