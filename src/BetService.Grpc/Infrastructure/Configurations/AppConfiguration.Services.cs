using BetService.BusinessLogic.Contracts.Providers;
using BetService.BusinessLogic.Contracts.Services;
using BetService.Grpc.Infrastructure.Clients.CashService;
using BetService.Grpc.Infrastructure.Mappings;
using ClientsWarapper = BetService.Grpc.Infrastructure.Clients;

namespace BetService.Grpc.Infrastructure.Configurations
{
    // TODO: Remove partial or rename AppConfiguration to AppConfigurations
    /// <summary>
    /// App configuration
    /// </summary>
    public static partial class AppConfiguration
    {
        /// <summary>Adds the infrastructure services to service collection</summary>
        /// <param name="services">The service collection.</param>
        /// <returns> IServiceCollection </returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(BetServiceProfile).Assembly);

            return services;
        }

        /// <summary>Adds the infrastructure services to service collection</summary>
        /// <param name="services">The service collection.</param>
        /// <returns> IServiceCollection  </returns>
        public static IServiceCollection AddBusinessLogicServices(this IServiceCollection services)
        {
            services.AddScoped<IBetService, BusinessLogic.Services.BetService>()
                .AddScoped<IDateTimeProvider, BusinessLogic.DateTimeProvider>();

            return services;
        }

        /// <summary>
        /// Add grpc services wrappers.
        /// </summary>
        /// <param name="services"></param>
        /// <returns> IServiceCollection </returns>
        public static IServiceCollection AddGrpcClientsServicesWrappers(this IServiceCollection services)
        {
            services.AddScoped<ICashService, ClientsWarapper.CashService.CashService>();

            return services;
        }
    }
}
