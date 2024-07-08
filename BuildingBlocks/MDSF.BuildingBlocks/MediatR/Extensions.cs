using MDSF.BuildingBlocks.Caching;
using MDSF.BuildingBlocks.Validation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MDSF.BuildingBlocks.MediatR;

public static class Extensions
{
    public static IServiceCollection AddCustomMediatR(this IServiceCollection services, params Assembly[] assemblies)
    {
        //services.AddMediatR(assemblies);
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        //services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));

        return services;
    }
}