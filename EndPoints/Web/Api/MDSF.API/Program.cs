using MDSF.BuildingBlocks;
using MDSF.BuildingBlocks.CAP;
using MDSF.BuildingBlocks.Endpoints.Web;
using MDSF.BuildingBlocks.Exceptions;
using MDSF.BuildingBlocks.Logging.Web;
using MDSF.BuildingBlocks.Options;
using MDSF.BuildingBlocks.Security;
using MDSF.BuildingBlocks.Swagger;
using MDSF.CrossDomains;
using MDSF.Customer;
using MDSF.Operator;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Serilog;

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

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCustomSwagger(configuration,
    typeof(CustomerStartup).Assembly,
    typeof(OperatorStartup).Assembly);



builder.Services.AddBuildingBlocksServices(configuration);
builder.Services.AddCrossDomainsServices(configuration);
builder.Services.AddCustomerServices(configuration);
builder.Services.AddOperatorServices(configuration);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();
app.UseExceptionHandler();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var provider = app.Services.GetService<IApiVersionDescriptionProvider>();
    app.UseCustomSwagger(provider);
}

app.UseSerilogRequestLogging();
app.UseCorrelationId();
app.UseRouting();
app.UseAuthorization();
app.UseHttpsRedirection();

app.UseCusomerDomain();
app.UseOperatorDomein();


app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapMagicOnionService();
});
app.MapGet("/", x => x.Response.WriteAsync(appOptions.Name));

app.Use(async (context, next) =>
{
    var authenticatedUserService = context.RequestServices.GetRequiredService<IAuthenticatedUser>();
    authenticatedUserService.Id = 124578;//TODO:JUST FOR DEMO

    await next();
});

app.Run();
