using AutoparkDAL.Entities;
using AutoparkDAL.Interfaces;
using AutoparkDAL.Repositories;
using WebApplication;

namespace Web_Autopark
{
    public class Startup
    {
        private readonly AppConfig _config;

        public Startup(IConfiguration configuration)
        {
            _config = new AppConfig(configuration);
            Console.WriteLine("Connection string: " + _config.ConnectionString); //Remove this
            _config.ConnectionString = "Server=localhost; Database=AutoparkDb; User Id=sa; Password=Orsi4ek148; TrustServerCertificate=true"; // it's better to move connection string to appsettings.json
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<IRepository<Vehicle>, VehicleRepository>(provider =>
                new VehicleRepository(_config.ConnectionString));
            services.AddTransient<IRepository<VehicleType>, VehicleTypeRepository>(provider =>
                new VehicleTypeRepository(_config.ConnectionString));
            services.AddTransient<IRepository<Component>, ComponentRepository>(provider =>
                new ComponentRepository(_config.ConnectionString));
            services.AddTransient<IRepository<Order>, OrderRepository>(provider =>
                new OrderRepository(_config.ConnectionString));
            services.AddTransient<IRepository<OrderItem>, OrderItemRepository>(provider =>
                new OrderItemRepository(_config.ConnectionString));
            services.AddControllersWithViews();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}