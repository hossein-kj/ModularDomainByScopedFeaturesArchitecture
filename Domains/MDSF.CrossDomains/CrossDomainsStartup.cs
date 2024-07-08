using MDSF.BuildingBlocks.Exceptions;
using MDSF.CrossDomains.Globalization;
using MDSF.CrossDomains.GRPC;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MDSF.CrossDomains
{
    //TODO:Add Service for CrossDomainsStartup
    public static class CrossDomainsStartup
    {
        public static IServiceCollection AddCrossDomainsServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<ITextResourceCrossDomains, TextResource>();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())); //services.AddCustomMediatR(typeof(BuildingBlocksStartup).Assembly);
            services.Configure<GrpcOptions>(options => configuration.GetSection("Grpc").Bind(options));

            services.AddGrpc(options =>
            {
                options.Interceptors.Add<GrpcExceptionInterceptor>();
            });

            services.AddMagicOnion();
            return services;
        }
    }
}
