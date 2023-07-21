using BetService.DataAccess.Extensions;
using BetService.Grpc.Infrastructure.Configurations;
using BetService.Grpc.Infrastructure.Interceptors;
using BetService.Grpc.Settings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args)
    .AddAppSettings()
    .AddSerilogLogger();

var configuration = builder.Configuration;

builder.Services.Configure<ServiceEndpointsSettings>(
    builder.Configuration.GetSection("ServiceEndpointsSettings"));

builder.Services
    .AddProviders()
    .AddRepositories()
    .AddPostgresContext(options =>
    {
        var connectionString = configuration.GetConnectionString("BetDb");
        options.UseNpgsql(connectionString);
    })
    .AddInfrastructureServices()
    .AddBusinessLogicServices()
    .AddGrpc(options =>
    {
        options.Interceptors.Add<ErrorHandlingInterceptor>();
        options.Interceptors.Add<ValidationInterceptor>();
    })
    .Services
    .AddGrpcClients();

var app = builder.Build();

app.MapGrpcService<BetService.Grpc.Services.BetService>();

app.Run();

namespace BetService.Grpc
{
    public partial class Program
    { }
}
