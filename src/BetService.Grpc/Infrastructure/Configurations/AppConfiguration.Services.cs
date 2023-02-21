using BetService.BusinessLogic.Contracts.Providers;
using BetService.BusinessLogic.Contracts.Services;
using BusinessLogic = BetService.BusinessLogic;

namespace BetService.Grpc.Infrastructure.Configurations
{
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
    }
}
