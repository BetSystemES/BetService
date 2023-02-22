using BetService.BusinessLogic.Enums;
using BetService.BusinessLogic.Models;

namespace BetService.BusinessLogic.Contracts.DataAccess.Repositories
{
    /// <summary>
    /// Bet repository contract.
    /// </summary>
    public interface IBetRepository : IDataRepository<Bet>
    {
        /// <summary>
        /// Update bet status by specific coefficient identifier.
        /// </summary>
        /// <param name="coefficientId"></param>
        /// <param name="betStatusType"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Task</returns>
        Task UpdateBetStatusByCoefficientId(Guid coefficientId, BetStatusType betStatusType, CancellationToken cancellationToken);
    }
}
