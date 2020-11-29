using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WST.Admin.Models;
using WST.Admin.Models.Repositories;
using WST.Admin.Profiles;

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
            services.AddDbContext<IdentityDbContext>(options => options.UseNpgsql(Configuration["Data:identity:ConnectionString"]));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();
            
            services.AddSingleton<IElectricLocomotiveRepository, ElectricLocomotiveRepository>();
            services.AddSingleton<IBreakingRepository, BreakingRepository>();
            services.AddSingleton<IBreakingImageRepository, BreakingImageRepository>();
            services.AddSingleton<IDetailRepository, DetailRepository>();
            
            services.AddSingleton(new MapperConfiguration(RegisterMapping).CreateMapper());
            
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

            app.UseAuthentication();
            
            app.UseMvc(MapRoutes);
            
            MigrationHelper.Migrate(app);

            IdentitySeedData.EnsurePopulated(app).Wait();
        }
        
        private static void MapRoutes(IRouteBuilder routes)
        {
            routes.MapRoute(name: null, template: "/", defaults: new {controller = "ElectricLocomotive", action = "Index", page = 1});
            
            routes.MapRoute(
                name: null,
                template: "{controller}/{action}/{id?}");
        }

        private static void RegisterMapping(IMapperConfigurationExpression cfg)
        {
            cfg.AddProfile(new MapperProfile());
        }
    }
}