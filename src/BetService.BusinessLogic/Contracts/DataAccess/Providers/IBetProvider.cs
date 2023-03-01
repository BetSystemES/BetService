using BetService.BusinessLogic.Models;

namespace BetService.BusinessLogic.Contracts.DataAccess.Providers
{
    // TODO: change file location to BetService.DataAccess.Contracts.Providers
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
    }
}
