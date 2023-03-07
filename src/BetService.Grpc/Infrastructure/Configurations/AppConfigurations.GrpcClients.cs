using BetService.Grpc.Settings;
using Microsoft.Extensions.Options;
using static CashService.GRPC.CashService;

namespace BetService.Grpc.Infrastructure.Configurations
{
    public static partial class AppConfigurations
    {
        public static IServiceCollection AddGrpcClients(this IServiceCollection services)
        {
            services
                .AddGrpcClient<CashServiceClient>();

            return services;
        }

        private static IServiceCollection AddGrpcClient<T>(this IServiceCollection services) where T : class
        {
            var serviceEndpointsSettings = services.BuildServiceProvider().GetService<IOptions<ServiceEndpointsSettings>>()!.Value;

            var serviceName = typeof(T).Name;

            var serviceEndpoint = serviceEndpointsSettings?.ServiceEndpoints
                .FirstOrDefault(x => x.Name.Equals(serviceName));

            ArgumentNullException.ThrowIfNull(serviceEndpoint, nameof(serviceEndpoint));

            return services
                .AddGrcpServiceClient<T>(serviceName, serviceEndpoint.Url);
        }

        private static IServiceCollection AddGrcpServiceClient<TClient>(this IServiceCollection services, string clientName, string endpoint) where TClient : class
        {
            return services
                .AddGrpcClient<TClient>(clientName, options =>
                {
                    options.Address = new Uri(endpoint);
                })
                .Services;
        }
    }
}
