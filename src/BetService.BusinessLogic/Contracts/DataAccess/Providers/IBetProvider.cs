using System.Collections.Generic;
using BetService.BusinessLogic.Models;

namespace BetService.BusinessLogic.Contracts.DataAccess.Providers
{
    /// <summary>
    /// Bet provider contract.
    /// </summary>
    public interface IBetProvider
    {
        /// <summary>
        /// Gets the bet by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Bet?</returns>
        Task<Bet?> GetBetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the bets range by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The list of bets.</returns>
        Task<List<Bet>> GetBetsRangeByUserId(Guid userId, int page, int pageSize, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the range by coefficient identifier.
        /// </summary>
        /// <param name="coefficientId">The coefficient identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The list of <seealso cref="Bet"/>s with specific coefficientId.</returns>
        Task<IEnumerable<Bet>> GetRangeByCoefficientId(Guid coefficientId, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the range by coefficient identifiers.
        /// </summary>
        /// <param name="coefficientIds">The coefficient identifiers.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The list of <seealso cref="Bet"/>s with specific coefficientId.</returns>
        Task<IEnumerable<Bet>> GetRangeByCoefficientIds(IEnumerable<Guid> coefficientIds, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the range.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns> Range of bets with specific ids. </returns>
        Task<IEnumerable<Bet>> GetRange(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    }
}
