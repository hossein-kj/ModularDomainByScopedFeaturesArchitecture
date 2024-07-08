using MDSF.BuildingBlocks.MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MDSF.Operator.CrossDomainFeatures.ConfirmRegistration
{
    public static partial class ConfirmRegistration
    {
        internal static IServiceCollection AddConfirmRegistrationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IConfirmRegistrationRegistrationAdoContext, ConfirmRegistrationRegistrationAdoContext>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())); //services.AddCustomMediatR(typeof(ConfirmRegistration).Assembly);
            services.AddCustomMediatR(Assembly.GetExecutingAssembly());
            return services;
        }

    }
}
