using BetService.BusinessLogic.Entities;
using BetService.BusinessLogic.Enums;

namespace BetService.BusinessLogic.Contracts.DataAccess.Repositories
{
    /// <summary>
    /// Bet repository contract.
    /// </summary>
    public interface IBetRepository : IDataRepository<Bet>
    {
        public Task UpdateBetStatuseByCoefficientIds(
            IEnumerable<Guid> ids, BetStatusType status, CancellationToken token);

        public Task UpdateBetStatuseAndPayoutStatusByCoefficientIds(
            IEnumerable<Guid> ids, BetStatusType status, BetPayoutStatus betPayoutStatus, CancellationToken token);
    }
}
