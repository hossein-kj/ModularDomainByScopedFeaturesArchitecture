using MDSF.BuildingBlocks.MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MDSF.CrossDomains.Tasks.Customer.ConsumeService;

public static partial class ConsumeService
{
    internal static IServiceCollection AddRegistrationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())); //services.AddCustomMediatR(typeof(Registration).Assembly);
        services.AddCustomMediatR(Assembly.GetExecutingAssembly());
        return services;
    }

    internal static IApplicationBuilder UseRegistrationFeatures(this IApplicationBuilder app)
    {
        return app;
    }

}
