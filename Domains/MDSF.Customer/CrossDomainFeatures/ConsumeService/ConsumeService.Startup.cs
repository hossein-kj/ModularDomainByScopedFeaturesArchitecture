using MDSF.BuildingBlocks.MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MDSF.Customer.CrossDomainFeatures.ConsumeService
{
    public static partial class ConsumeService
    {
        public static IServiceCollection AddConsumeServiceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IConsumeServiceAdoContext, ConsumeServiceAdoContext>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())); //services.AddCustomMediatR(typeof(ApplyService).Assembly);
            services.AddCustomMediatR(Assembly.GetExecutingAssembly());
            return services;
        }

    }
}
