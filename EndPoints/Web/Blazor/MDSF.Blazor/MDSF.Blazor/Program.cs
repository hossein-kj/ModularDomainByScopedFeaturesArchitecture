using MDSF.Blazor.Client.Pages;
using MDSF.Blazor.Components;
using MDSF.BuildingBlocks.Options;
using MDSF.BuildingBlocks.Security;
using MDSF.BuildingBlocks.Logging.Web;
using MDSF.BuildingBlocks.Endpoints.Web;
using MDSF.BuildingBlocks.Exceptions;
using MDSF.BuildingBlocks;
using MDSF.CrossDomains;
using MDSF.Customer;
using MDSF.Operator;
using Serilog;
using MDSF.BuildingBlocks.CAP;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var env = builder.Environment;

var appOptions = builder.Services.GetOptions<AppOptions>("AppOptions");
builder.Services.Configure<AppOptions>(options => configuration.GetSection("AppOptions").Bind(options));
Console.WriteLine(appOptions.Name);

// Add services to the container.
builder.AddCustomSerilog(env);
builder.Services.AddScoped<IAuthenticatedUser, AuthenticatedUser>();
builder.Services.AddControllers();
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


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseSerilogRequestLogging();
app.UseCorrelationId();
app.UseRouting();
app.UseAntiforgery();
app.UseAuthorization();


app.UseCusomerDomain();
app.UseOperatorDomein();


app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapMagicOnionService();
});


app.Use(async (context, next) =>
{
    var authenticatedUserService = context.RequestServices.GetRequiredService<IAuthenticatedUser>();
    authenticatedUserService.Id = 124578;//TODO:JUST FOR DEMO

    await next();
});



app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);

app.Run();
