using FluentValidation;
using MDSF.BuildingBlocks.Caching;
using MDSF.BuildingBlocks.Data.EFCore;
using MDSF.BuildingBlocks.IdsGenerator;
using MDSF.BuildingBlocks.MediatR;
using MDSF.Customer.CrossDomainFeatures.ConsumeService;
using MDSF.Customer.Data.EF;
using MDSF.Customer.Features.Registration;
using MDSF.Customer.Globalization;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace MDSF.Customer
{
    //TODO:Add Service for BuildingBlocksStartup
    public static class CustomerStartup
    {
        public static IServiceCollection AddCustomerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCustomDbContext<ICustomerDbContext, CustomerDbContext>(nameof(Customer), configuration);
            SnowFlakIdGenerator.Configure(3);

            services.AddOptions<RazorViewEngineOptions>()
         .Configure<IOptions<PluginViewOptions>>((options, settings) =>
         {
             options.ViewLocationExpanders.Add(new PluginViewLocationExpander(settings));
         });

            var builder = services.AddMvc();
            builder.Services.AddRazorPages();
            builder.Services.AddRazorComponents();
            builder.AddApplicationPart(typeof(CustomerStartup).Assembly);

            services.AddScoped<ITextResourceCustomer, TextResource>();
            services.AddRegistrationServices(configuration);
            services.AddConsumeServiceServices(configuration);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly())); //services.AddCustomMediatR(typeof(CustomerStartup).Assembly);
            services.AddCustomMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddCachingRequest(new List<Assembly> { typeof(CustomerStartup).Assembly });

            services.Scan(scan => scan
   .FromAssemblies(typeof(CustomerStartup).Assembly)
   .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)),
       false)
   .AsImplementedInterfaces()
   .WithLifetime(ServiceLifetime.Scoped));


            return services;
        }

        public static IApplicationBuilder UseCusomerDomain(this IApplicationBuilder app)
        {
            app.UseRegistrationFeatures();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            return app;
        }
    }

    internal sealed class PluginViewLocationExpander : IViewLocationExpander
    {
        private readonly PluginViewOptions _options;

        public PluginViewLocationExpander(IOptions<PluginViewOptions> options)
        {
            _options = options.Value;
        }

        private const string Key = "Customer";

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            if (!string.IsNullOrEmpty(_options.Location))
                context.Values[Key] = _options.Location;
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            var themeLocations = new List<string>();
            if (context.Values.TryGetValue(Key, out string location))
            {
                themeLocations.Add($"/CustomerViews/Registration/{location}.cshtml");
                viewLocations = viewLocations.Concat(themeLocations);
            }
            return viewLocations;
        }
    }
    internal sealed class PluginViewOptions
    {
        /// <summary>
        /// Gets or sets the location name for the theme
        /// </summary>
        public string Location { get; set; } = "Privacy";
    }
}

