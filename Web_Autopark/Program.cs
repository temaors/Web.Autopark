using System.Data;
using AutoparkDAL.Entities;
using AutoparkDAL.Interfaces;
using AutoparkDAL.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Web_Autopark;


var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddTransient<IRepository<Component>, ComponentRepository>(provider => new ComponentRepository(connection));
builder.Services.AddTransient<IRepository<Order>, OrderRepository>(provider => new OrderRepository(connection));
builder.Services.AddTransient<IRepository<Vehicle>, VehicleRepository>(provider => new VehicleRepository(connection));
builder.Services.AddTransient<IRepository<VehicleType>, VehicleTypeRepository>(provider => new VehicleTypeRepository(connection));
builder.Services.AddTransient<IRepository<OrderItem>, OrderItemRepository>(provider => new OrderItemRepository(connection));
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); 
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