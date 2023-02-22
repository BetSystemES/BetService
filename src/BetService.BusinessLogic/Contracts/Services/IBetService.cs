using BetService.BusinessLogic.Models;

namespace BetService.BusinessLogic.Contracts.Services
{
    /// <summary>
    /// Bet service contract.
    /// </summary>
    public interface IBetService
    {
        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The bet.</returns>
        Task<Bet?> GetById(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the range by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="page">The page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The list of bets.</returns>
        Task<List<Bet>> GetRangeByUserId(Guid userId, int page, int pageSize, CancellationToken cancellationToken);

        /// <summary>
        /// Updates the statuses.
        /// </summary>
        /// <param name="betStatusUpdateModels">The bet status update models.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task</returns>
        Task UpdateStatuses(List<BetStatusUpdateModel> betStatusUpdateModels, CancellationToken cancellationToken);

        /// <summary>
        /// Creates the specified bet.
        /// </summary>
        /// <param name="bet">The bet.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task</returns>
        Task Create(Bet bet, CancellationToken cancellationToken);

        /// <summary>
        /// Creates the range.
        /// </summary>
        /// <param name="bets">The bets.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task</returns>
        Task CreateRange(List<Bet> bets, CancellationToken cancellationToken);
    }
}
