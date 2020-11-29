using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WST.Admin.Models;
using WST.Admin.Models.Repositories;
using WST.Admin.Services;

namespace WST.Admin
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<WstDbContext>(options => options.UseNpgsql(Configuration["Data:wst:ConnectionString"]));

            services.AddSingleton<IElectricLocomotiveRepository, ElectricLocomotiveRepository>();
            services.AddSingleton<IBreakingRepository, BreakingRepository>();
            services.AddSingleton<IBreakingImageRepository, BreakingImageRepository>();
            
            // services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();

            app.UseStaticFiles();

            app.UseSession();
            
            app.UseMvc(MapRoutes);
            
            MigrationHelper.Migrate(app);
        }
        
        private static void MapRoutes(IRouteBuilder routes)
        {
            // routes.MapRoute(
            //     name: null, 
            //     template: "{category}/Page{productPage:int}", 
            //     defaults: new {controller = "Product", action = "List"});
            //
            // routes.MapRoute(
            //     name: null,
            //     template: "Page{productPage:int}",
            //     defaults: new { controller = "Product", action = "List", productPage = 1 });
            //
            // routes.MapRoute(
            //     name: null,
            //     template: "{category}",
            //     defaults: new { controller = "Product", action = "List", productPage = 1 });
            //
            // routes.MapRoute( 
            //     name: null,
            //     template: "",
            //     defaults: new { controller = "Product", action = "List", productPage = 1 });
            
            // ElectricLocomotiveController

            routes.MapRoute(name: null, template: "/", defaults: new {controller = "ElectricLocomotive", action = "Index", page = 1});
            
            routes.MapRoute(
                name: null,
                template: "{controller}/{action}/{id?}");
        }
    }
}