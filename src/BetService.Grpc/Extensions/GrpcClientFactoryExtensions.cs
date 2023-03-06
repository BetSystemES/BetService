using Grpc.Net.ClientFactory;

namespace BetService.Grpc.Extensions;

public static class GrpcClientFactoryExtensions
{
    public static T GetGrpcClient<T>(this GrpcClientFactory grpcClientFactory) where T : class
    {
        return grpcClientFactory.CreateClient<T>(nameof(T));
    }
}
