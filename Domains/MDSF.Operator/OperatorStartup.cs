using FluentValidation;
using MDSF.BuildingBlocks.Caching;
using MDSF.BuildingBlocks.IdsGenerator;
using MDSF.BuildingBlocks.MediatR;
using MDSF.Operator.CrossDomainFeatures.ConfirmRegistration;
using MDSF.Operator.Features.ApplyService;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MDSF.Operator
{
    //TODO:Add Service for BuildingBlocksStartup
    public static class OperatorStartup
    {
        public static IServiceCollection AddOperatorServices(this IServiceCollection services, IConfiguration configuration)
        {
            SnowFlakIdGenerator.Configure(3);

            services.AddConfirmRegistrationServices(configuration);
            services.AddApplyServiceServices(configuration);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())); //services.AddCustomMediatR(typeof(OperatorStartup).Assembly);
            services.AddCustomMediatR(Assembly.GetExecutingAssembly());
            services.AddCachingRequest(new List<Assembly> { typeof(OperatorStartup).Assembly });

            services.Scan(scan => scan
   .FromAssemblies(typeof(OperatorStartup).Assembly)
   .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)),
       false)
   .AsImplementedInterfaces()
   .WithLifetime(ServiceLifetime.Scoped));


            return services;
        }

        public static IApplicationBuilder UseOperatorDomein(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            return app;
        }
    }
}
