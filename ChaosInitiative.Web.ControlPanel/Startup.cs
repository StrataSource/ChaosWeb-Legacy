using AspNet.Security.OAuth.GitHub;
using ChaosInitiative.Web.Shared;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ChaosInitiative.Web.ControlPanel
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationContext>(options =>
            {
                if (Environment.IsDevelopment())
                    options.UseSqlite("DataSource=Chaos.db");
                else
                    options.UseMySql(Secrets.Get("db.connect"));
            });

            services.AddDbContext<IdentityDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDb");
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                //options.RequireAuthenticatedSignIn = false;
            }).AddCookie(options =>
            {
                options.LoginPath = "/Auth/Login";
                options.LogoutPath = "/Auth/Logout";
                options.AccessDeniedPath = "/Auth/AccessDenied";
            }).AddGitHub(
                options =>
                {
                    //options.ClientId = Secrets.Get("oauthClientId", Environment.IsDevelopment() ? DeploymentType.Development : DeploymentType.Production);
                    //options.ClientSecret = Secrets.Get("oauthClientSecret", Environment.IsDevelopment() ? DeploymentType.Development : DeploymentType.Production);
                    options.ClientId = "Iv1.89b774c7a544eeb0";
                    options.ClientSecret = "4c5e74f8860300767710ab6f54d9184cc4fc2537";

                    options.ForwardSignOut = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.CallbackPath = "/Auth/LoggedIn";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Member", policy =>
                {
                    policy.RequireClaim("Member");
                    policy.AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme);
                });
            });
            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<IdentityDbContext>();

            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeFolder("/Panel", "Member");
                options.Conventions.AllowAnonymousToPage("/Index");
            }).AddRazorRuntimeCompilation();

            services.AddControllersWithViews();
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