using BetService.BusinessLogic.Contracts.Providers;
using BetService.BusinessLogic.Contracts.Services;
using BetService.Grpc.Infrastructure.Mappings;

namespace BetService.Grpc.Infrastructure.Configurations
{
    /// <summary>
    /// App configuration
    /// </summary>
    public static partial class AppConfigurations
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
                .AddScoped<IDateTimeProvider, BusinessLogic.Providers.DateTimeProvider>();

            return services;
        }
    }
}
