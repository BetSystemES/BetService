using CashService.GRPC;
using Grpc.Net.ClientFactory;
using static CashService.GRPC.CashService;
using BusinessModels = BetService.BusinessLogic.Models;

namespace BetService.Grpc.Infrastructure.Clients.CashService
{
    public class CashService : ICashService
    {
        private readonly GrpcClientFactory _grpcClientFactory;

        public CashService(GrpcClientFactory grpcClientFactory)
        {
            _grpcClientFactory = grpcClientFactory;
        }

        public async Task DepositRange(IEnumerable<BusinessModels.Bet> bets, CancellationToken cancellationToken)
        {
            var cashServiceClient = _grpcClientFactory.CreateClient<CashServiceClient>(nameof(CashServiceClient));

            var request = new DepositRangeRequest();

            foreach (var bet in bets)
            {
                var transactionModel = new TransactionModel
                {
                    ProfileId = bet.UserId.ToString()
                };
                transactionModel.Transactions.Add(new Transaction()
                {
                    Amount = bet.Amount,
                    CashType = CashType.Cash,
                    // TODO: delete generation of transactionId
                    TransactionId = Guid.NewGuid().ToString(),
                });

                request.DepositRangeRequests.Add(transactionModel);
            }

            var response = await cashServiceClient.DepositRangeAsync(request);
        }
    }
}
