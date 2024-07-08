using MDSF.BuildingBlocks.Logging.Win;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MDSF.BuildingBlocks.Endpoints.Win
{
    public static class WindowsServiceCollectionManager
    {
        public static ServiceCollection Services { get; set; }
        public static IConfigurationRoot Configuration { get; set; }
        public static ServiceProvider ServiceProvider { get; set; }
        public static void Build()
        {
            Services.AddScoped(typeof(Microsoft.Extensions.Logging.ILogger<>), typeof(WindowsLogger<>));
            Services.AddScoped<Microsoft.Extensions.Logging.ILoggerFactory, WindowsLogFactory>();
            ServiceProvider = Services.BuildServiceProvider();
        }
        static WindowsServiceCollectionManager()
        {
            Services = new ServiceCollection();
            Configuration = new ConfigurationBuilder()
  .AddInMemoryCollection(new Dictionary<string, string?>()
  {
      ["ConnectionStrings:Customer"] = "Server=(localdb)\\mssqllocaldb;Database=MDSF;Trusted_Connection=True;MultipleActiveResultSets=true",
      ["ConnectionStrings:ConsumeService"] = "Server=(localdb)\\mssqllocaldb;Database=MDSF;Trusted_Connection=True;MultipleActiveResultSets=true",
      ["ConnectionStrings:ConfirmRegistration"] = "Server=(localdb)\\mssqllocaldb;Database=MDSF;Trusted_Connection=True;MultipleActiveResultSets=true",
      ["ConnectionStrings:ApplyService"] = "Server=(localdb)\\mssqllocaldb;Database=MDSF;Trusted_Connection=True;MultipleActiveResultSets=true",
      ["AppOptions:Name"] = "ModularDomainByScopedFeaturesArchitect",
      ["AppOptions:CustomerIsMicroService"] = "false",
      ["AppOptions:OperatorIsMicroService"] = "false",
      ["AppOptions:AdminIsMicroService"] = "false"
  })
.Build();
            Services.AddSingleton<IConfiguration>(Configuration);
        }
    }
}
