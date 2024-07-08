using MDSF.BuildingBlocks.CAP;
using MDSF.BuildingBlocks.Exceptions;
using MDSF.BuildingBlocks.Options;
using MDSF.BuildingBlocks.Security;
using MDSF.BuildingBlocks.Logging.Web;
using MDSF.BuildingBlocks.Endpoints.Web;
using MDSF.BuildingBlocks;
using MDSF.CrossDomains;
using MDSF.Customer;
using MDSF.Operator;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var configuration = builder.Configuration;
var env = builder.Environment;

var appOptions = builder.Services.GetOptions<AppOptions>("AppOptions");
builder.Services.Configure<AppOptions>(options => configuration.GetSection("AppOptions").Bind(options));
Console.WriteLine(appOptions.Name);

// Add services to the container.
builder.AddCustomSerilog(env);
builder.Services.AddScoped<IAuthenticatedUser, AuthenticatedUser>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddCustomCap();
builder.Services.AddTransient<IBusPublisher, BusPublisher>();
builder.Services.AddCustomVersioning();

builder.Services.AddBuildingBlocksServices(configuration);
builder.Services.AddCrossDomainsServices(configuration);
builder.Services.AddCustomerServices(configuration);
builder.Services.AddOperatorServices(configuration);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseExceptionHandler();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSerilogRequestLogging();
app.UseCorrelationId();
app.UseRouting();
app.UseAntiforgery();
app.UseAuthorization();


app.UseCusomerDomain();
app.UseOperatorDomein();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapMagicOnionService();
});



app.Run();
