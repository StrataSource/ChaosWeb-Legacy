using AspNet.Security.OAuth.GitHub;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChaosInitiative.Web.ControlPanel
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
            services.AddAuthentication(GitHubAuthenticationDefaults.AuthenticationScheme)
                .AddCookie()
                .AddGitHub(options =>
                    {
                        //options.ClientId = "Iv1.14d48eeeba4723c3";
                        //options.ClientSecret = "be5d2d6184401a484b07d4a15f6c7e64a3f2b134";
                        options.ClientId = "Iv1.89b774c7a544eeb0";
                        options.ClientSecret = "";
                        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                        options.AccessDeniedPath = "/OhNo";
                        options.CallbackPath = "/Panel/LoggedIn";
                    });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(PartOfOrganizationAuthorizeAttribute.POLICY_NAME, policy =>
                {
                    
                });
            });
            
            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeFolder("/Panel");
                options.Conventions.AllowAnonymousToPage("/Index");
            });
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });
        }
    }
}