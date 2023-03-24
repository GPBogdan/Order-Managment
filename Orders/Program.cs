using Microsoft.EntityFrameworkCore;
using DAL.Models;
using DAL.Repositories;
using BLL.Services;

var builder = WebApplication.CreateBuilder(args);

//SQL Dependency Injection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<OrdersContext>(x => x.UseLazyLoadingProxies().UseSqlServer(connectionString));

//Add Service Injection
builder.Services.AddScoped<IRepository<Order>, OrderRepository>();
builder.Services.AddScoped<OrderService, OrderService>();
builder.Services.AddScoped<IRepository<OrderItem>, OrderItemRepository>();
builder.Services.AddScoped<OrderItemService, OrderItemService>();
builder.Services.AddScoped<IRepository<Provider>, ProviderRepository>();
builder.Services.AddScoped<ProviderService, ProviderService>();


builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
