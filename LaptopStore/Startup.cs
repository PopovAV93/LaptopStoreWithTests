using LaptopStore.Data;
using LaptopStore.Data.Interfaces;
using LaptopStore.Data.Mocks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaptopStore.Data.Repository;
using Microsoft.AspNetCore.Http;
using LaptopStore.Data.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using NLog;
using NLog.Config;
using NLog.Extensions.Logging;

namespace LaptopStore
{
    public class Startup
    {
        private IConfigurationRoot _confString;
        public Startup(IHostEnvironment hostEnv)
        {
            _confString = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("dbsettings.json").Build();
        }
        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddLogging(builder => builder.AddNLogWeb("nlog.config"));
            
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                    options.AccessDeniedPath = new PathString("/Account/Login");
                });
            
            string connectionString = _confString.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDBContent>(builder => builder.UseSqlServer(connectionString,
                b => b.MigrationsAssembly("LaptopStore")));
            services.AddRazorPages();

            services.AddTransient<ILaptops, LaptopRepository>();
            services.AddTransient<ICategory, CategoryRepository>();
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IUsers, UserRepository>();
            services.AddScoped<IProfiles, ProfileRepository>();
            services.AddScoped<IOrders, OrderRepository>();
            services.AddScoped<IAccounts, AccountRepository>();
            services.AddScoped(c => Cart.GetCart(c));

            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            /*
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            */

            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Pages/Error");
                app.UseHsts();
            }

            //app.UseHttpLogging();
            //app.UseHttpLogging();
            app.UseHttpsRedirection();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllerRoute
                (
                    name: "laptops", 
                    pattern: "Laptops/{action}/{category?}",
                    defaults: new { controller = "Laptops", action = "List" }
                );
                endpoints.MapControllerRoute
                (
                    name: "profile",
                    pattern: "Profile/{action}/{profile?}",
                    defaults: new { controller = "Profile", action = "ProfileInfo" }
                );
                endpoints.MapControllerRoute
                (
                    name: "user",
                    pattern: "User/{action}/{user?}",
                    defaults: new { controller = "User", action = "GetUsers" }
                );
                endpoints.MapControllerRoute
                (
                    name: "order",
                    pattern: "Order/{action}/{order?}",
                    defaults: new { controller = "Order", action = "DeleteOrder" }
                );
            });

            using (var scope = app.ApplicationServices.CreateScope())
            {
                AppDBContent content = scope.ServiceProvider.GetRequiredService<AppDBContent>();
                content.Database.EnsureCreated();
                DBObjects.Initial(content);

            }
        }
    }
}
