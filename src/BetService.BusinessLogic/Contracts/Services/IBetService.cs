﻿using BetService.BusinessLogic.Entities;

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
        /// <returns>The range of bets.</returns>
        Task<IEnumerable<Bet>> GetRangeByUserId(Guid userId, int page, int pageSize, CancellationToken cancellationToken);

        /// <summary>
        /// Updates the statuses. For positive <seealso cref="Enums.BetStatusType"/> also modify payout status type for <seealso cref="Enums.BetPayoutStatus.Processing"/>.
        /// </summary>
        /// <param name="betStatusUpdateModels">The bet status update models.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task </returns>
        Task UpdateStatuses(IEnumerable<BetStatusUpdateModel> betStatusUpdateModels, CancellationToken cancellationToken);

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

        /// <summary>
        /// Gets the range by coefficient identifier.
        /// </summary>
        /// <param name="coefficientId">The coefficient identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The range of bets.<seealso cref="Bet"/>s with specific coefficientId.</returns>
        Task<IEnumerable<Bet>> GetRangeByCoefficientId(Guid coefficientId, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the range by coefficient identifiers.
        /// </summary>
        /// <param name="coefficientIds">The coefficient identifiers.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The range of bets.<seealso cref="Bet"/>s with specific coefficientId.</returns>
        Task<IEnumerable<Bet>> GetRangeByCoefficientIds(IEnumerable<Guid> coefficientIds, CancellationToken cancellationToken);

        /// <summary>
        /// Completes the payout statues.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task</returns>
        Task CompletePayoutStatues(IEnumerable<Bet> entities, CancellationToken cancellationToken);

        /// <summary>
        /// Get the range of bets with <seealso cref="Bet.PayoutStatus"/> equals to '<seealso cref="Enums.BetPayoutStatus.Processing"/>'.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>The range of processing bets. </returns>
        Task<IEnumerable<Bet>> GetRangeProcessingBets(CancellationToken cancellationToken);

        /// <summary>
        /// Update bet statuses and get collection of bets with processing status
        /// </summary>
        /// <param name="betStatusUpdateModels"></param>
        /// <param name="token"></param>
        /// <returns>The range of bets.<Bet></returns>
        Task<IEnumerable<Bet>> HandleUpdateStatuses(IEnumerable<BetStatusUpdateModel> betStatusUpdateModels, CancellationToken token);

        Task<IEnumerable<Bet>> HandleUpdateStatus(BetStatusUpdateModel betStatusUpdateModel, CancellationToken token);
    }
}
