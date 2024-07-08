using MDSF.BuildingBlocks.MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MDSF.Operator.Features.ApplyService;

public static partial class ApplyService
{
    internal static IServiceCollection AddApplyServiceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())); //services.AddCustomMediatR(typeof(Registration).Assembly);
        services.AddCustomMediatR(Assembly.GetExecutingAssembly());
        return services;
    }

}
