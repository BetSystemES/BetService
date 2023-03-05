using BusinessModels = BetService.BusinessLogic.Models;

namespace BetService.Grpc.Infrastructure.Clients.CashService
{
    public interface ICashService
    {
        Task DepositRange(IEnumerable<BusinessModels.Bet> bets, CancellationToken cancellationToken);
    }
}
