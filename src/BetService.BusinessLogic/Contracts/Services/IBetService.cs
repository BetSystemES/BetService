using BetService.BusinessLogic.Models;

namespace BetService.BusinessLogic.Contracts.Services
{
    /// <summary>
    /// Bet service contract.
    /// </summary>
    public interface IBetService
    {
        Task<Bet?> GetById(Guid id, CancellationToken cancellationToken);

        Task<List<Bet>> GetRangeByUserId(Guid userId, int page, int pageSize, CancellationToken cancellationToken);

        Task UpdateStatuses(List<BetStatusUpdateModel> betStatusUpdateModels, CancellationToken cancellationToken);

        Task Create(Bet bet, CancellationToken cancellationToken);

        Task CreateRange(List<Bet> bets, CancellationToken cancellationToken);
    }
}
