﻿using BetService.BusinessLogic.Entities;

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
        Task<IEnumerable<Bet>> GetBetsRangeByUserId(Guid userId, int page, int pageSize, CancellationToken cancellationToken);

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
        /// Get the range of bets with <seealso cref="Bet.PayoutStatus"/> equals to '<seealso cref="Enums.BetPayoutStatus.Processing"/>'.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>The list of processing bets. </returns>
        Task<IEnumerable<Bet>> GetRangeProcessingBets(CancellationToken cancellationToken);
    }
}
