using ClassLibrary1.Entities;
using ClassLibrary1.Repositories;
using Web_Autopark.Interfaces;
using WebApplication;

namespace Web_Autopark
{
    public class Startup
    {
        private readonly AppConfig _config;

        public Startup(IConfiguration configuration)
        {
            _config = new AppConfig(configuration);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<IRepository<VehicleType>, VehiclesTypesRepository>(provider =>
                new VehiclesTypesRepository(_config.ConnectionString));
            services.AddTransient<IRepository<Vehicle>, VehicleRepository>(provider =>
                new VehicleRepository(_config.ConnectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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