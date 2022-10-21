using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

try
{
    IConfiguration configuration = new ConfigurationManager();
    string connectionString = configuration.GetConnectionString("Default");
    Console.WriteLine(connectionString);
    IDbConnection db = new SqlConnection(connectionString);
}
catch (Exception e)
{
    Console.WriteLine("Error: " + e.Message + " AND " + e.TargetSite);
}


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