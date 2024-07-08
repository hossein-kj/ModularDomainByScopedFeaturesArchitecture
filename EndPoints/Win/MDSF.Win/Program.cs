using DotNetCore.CAP;
using MDSF.BuildingBlocks;
using MDSF.BuildingBlocks.Endpoints.Win;
using MDSF.BuildingBlocks.Security;
using MDSF.CrossDomains;
using MDSF.Customer;
using MDSF.Operator;
using Microsoft.Extensions.DependencyInjection;
namespace MDSF.Win
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            WindowsServiceCollectionManager.Services.AddScoped<IAuthenticatedUser, AuthenticatedUser>();
            WindowsServiceCollectionManager.Services.AddBuildingBlocksServices(WindowsServiceCollectionManager.Configuration);
            WindowsServiceCollectionManager.Services.AddCrossDomainsServices(WindowsServiceCollectionManager.Configuration);
            WindowsServiceCollectionManager.Services.AddCustomerServices(WindowsServiceCollectionManager.Configuration);
            WindowsServiceCollectionManager.Services.AddOperatorServices(WindowsServiceCollectionManager.Configuration);
            WindowsServiceCollectionManager.Build();

            var authenticatedUserService = WindowsServiceCollectionManager.ServiceProvider.GetRequiredService<IAuthenticatedUser>();
            authenticatedUserService.Id = 124578;//TODO:JUST FOR DEMO

            var _bootstrapper = WindowsServiceCollectionManager.ServiceProvider.GetRequiredService<IBootstrapper>();
            _bootstrapper.BootstrapAsync(CancellationToken.None).ConfigureAwait(false);
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}