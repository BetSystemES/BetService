using BetService.BusinessLogic.Entities;
using BetService.BusinessLogic.Enums;

namespace BetService.BusinessLogic.Contracts.DataAccess.Repositories
{
    /// <summary>
    /// Bet repository contract.
    /// </summary>
    public interface IBetRepository : IDataRepository<Bet>
    {
        public Task UpdateBetStatusesByCoefficientIds(
            IEnumerable<Guid> ids, BetStatusType status, CancellationToken token);

        public Task UpdateBetStatusesAndPayoutStatusByCoefficientIds(
            IEnumerable<Guid> ids, BetStatusType status, BetPayoutStatus betPayoutStatus, CancellationToken token);

        public Task UpdateBetStatusesByCoefficientId(
            Guid id, BetStatusType status, CancellationToken token);

        public Task UpdateBetStatusesAndPayoutStatusByCoefficientId(
            Guid id, BetStatusType status, BetPayoutStatus betPayoutStatus, CancellationToken token);
    }
}
