using MDSF.BuildingBlocks.CAP;
using MDSF.BuildingBlocks.Globalization;
using MDSF.BuildingBlocks.MediatR;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MDSF.BuildingBlocks
{
    //TODO:Add Service for BuildingBlocksStartup
    public static class BuildingBlocksStartup
    {
        public static IServiceCollection AddBuildingBlocksServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEasyCaching(options => { options.UseInMemory(configuration, "mem"); });
            services.AddTransient<IBusPublisher, BusPublisher>();
            services.AddCustomCap();
            services.AddScoped<ITextResource, TextResource>();
            services.AddScoped<IMediatRService, MediatRService>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())); //services.AddCustomMediatR(typeof(BuildingBlocksStartup).Assembly);
            services.AddCustomMediatR(Assembly.GetExecutingAssembly());
            return services;
        }

        //public static IApplicationBuilder UseBookingModules(this IApplicationBuilder app)
        //{
        //    app.UseMigration<BookingDbContext>();
        //    app.UseEndpoints(endpoints =>
        //    {
        //        endpoints.MapMagicOnionService();
        //    });
        //    return app;
        //}
    }
}
