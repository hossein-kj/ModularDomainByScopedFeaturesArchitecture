using FluentValidation;
using MDSF.BuildingBlocks.Data.EFCore;
using MDSF.BuildingBlocks.MediatR;
using MDSF.Customer.Data.EF;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MDSF.Customer.Features.Registration;

public static partial class Registration
{
    internal static IServiceCollection AddRegistrationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())); //services.AddCustomMediatR(typeof(Registration).Assembly);
        services.AddCustomMediatR(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(typeof(Registration).Assembly);

        return services;
    }

    internal static IApplicationBuilder UseRegistrationFeatures(this IApplicationBuilder app)
    {
        app.UseMigration<CustomerDbContext>();
        return app;
    }

}
