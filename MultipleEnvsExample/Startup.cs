using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MultipleEnvsExample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void SharedConfigureServices(IServiceCollection services){
            services.AddControllersWithViews();
        }
        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            SharedConfigureServices(services);
        }
        public void ConfigureServices(IServiceCollection services)
        {
            SharedConfigureServices(services);
        }

        public void SharedConfigure(IApplicationBuilder app)
        {
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
        public void ConfigureDevelopment(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            SharedConfigure(app);
        }

        public void Configure(IApplicationBuilder app)
        {
           app.UseExceptionHandler("/Home/Error");
           app.UseHsts();
           SharedConfigure(app);
        }
    }
}
