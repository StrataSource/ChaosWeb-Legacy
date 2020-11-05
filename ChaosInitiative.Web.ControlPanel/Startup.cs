using System;
using System.Security.Claims;
using ChaosInitiative.Web.Shared;
using Microsoft.AspNetCore.Authentication.Cookies;
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
            
            GitHubUtil.Init(Environment.IsDevelopment() ? DeploymentType.Development : DeploymentType.Production);
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.IsEssential = true;
            });
            services.AddDbContext<ApplicationContext>(options =>
            {
                if (Environment.IsDevelopment())
                    options.UseSqlite(Secrets.Get("dbConnect", DeploymentType.Development));
                else
                    options.UseMySql(Secrets.Get("dbConnect", DeploymentType.Production));
            });

            services.AddDbContext<IdentityDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDb");
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Auth/Login";
                options.LogoutPath = "/Auth/Logout";
                options.AccessDeniedPath = "/Auth/AccessDenied";
            });
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsMember", policy =>
                {
                    policy.RequireClaim(ClaimTypes.Role, "Member");
                    policy.AddAuthenticationSchemes(CookieAuthenticationDefaults.AuthenticationScheme);
                });
            });
            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<IdentityDbContext>();

            services.AddRazorPages(options =>
            {
                options.Conventions.AuthorizeFolder("/Panel", "IsMember");
                options.Conventions.AllowAnonymousToPage("/Index");
                options.Conventions.AllowAnonymousToFolder("/Auth");
            }).AddRazorRuntimeCompilation();

            services.AddServerSideBlazor();
            
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

            app.UseSession();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
                endpoints.MapControllers();
            });
        }
    }
}