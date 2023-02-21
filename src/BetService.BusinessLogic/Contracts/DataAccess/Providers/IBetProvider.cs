using BetService.BusinessLogic.Models;

namespace BetService.BusinessLogic.Contracts.DataAccess.Providers
{
    /// <summary>
    /// Bet provider contract.
    /// </summary>
    public interface IBetProvider
    {
        Task<Bet?> GetBetById(Guid id, CancellationToken cancellationToken);

        Task<List<Bet>> GetBetsRangeByUserId(Guid userId, int page, int pageSize, CancellationToken cancellationToken);
    }
}
