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
            Console.WriteLine("Connection string: " + _config.ConnectionString);
            _config.ConnectionString = "Server=localhost; Database=AutoparkDb; User Id=sa; Password=Orsi4ek148; TrustServerCertificate=true";
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
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