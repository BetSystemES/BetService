using BetService.DataAccess;
using BetService.Grpc.Infrastructure.Configurations;
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
    .AddGrpcClientsServicesWrappers()
    .AddGrpc()
    .Services
    .AddGrpcClients();

var app = builder.Build();

app.MapGrpcService<BetService.Grpc.Services.BetService>();

app.Run();

namespace BetService.Grpc
{
    public partial class Program { }
}
