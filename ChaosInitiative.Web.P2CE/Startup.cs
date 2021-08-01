using ChaosInitiative.Web.P2CE.Pages;
using ChaosInitiative.Web.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChaosInitiative.Web.P2CE
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages()
#if DEBUG
                    .AddRazorRuntimeCompilation()
#endif
                ;
            
            services.AddControllers();
            services.AddHostedService<IssueCacheRefreshService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/NotFound");
                // The default HSTS value is 30 days. You may want to change this for production scenarios
                // See https://aka.ms/aspnetcore-hsts
                app.UseHsts();
            }

            app.UseStatusCodePagesWithRedirects("/NotFound");
            
            app.UsePhpRedirection();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}