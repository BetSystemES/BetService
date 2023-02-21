using BetService.DataAccess;
using BetService.Grpc.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args)
    .AddAppSettings()
    .AddSerilogLogger();

var configuration = builder.Configuration;

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
    .AddGrpc();

var app = builder.Build();

app.MapGrpcService<BetService.Grpc.Services.BetService>();

app.Run();

namespace BetService.Grpc
{
    public partial class Program { }
}
