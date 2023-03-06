using BetService.BusinessLogic.Contracts.DataAccess.Providers;
using BetService.BusinessLogic.Contracts.DataAccess.Repositories;
using BetService.BusinessLogic.Contracts.Services;
using BetService.BusinessLogic.Entities;
using BetService.BusinessLogic.Enums;
using BetService.BusinessLogic.Extensions;
using BetService.DataAccess.Contracts;
using Microsoft.Extensions.Logging;

namespace BetService.BusinessLogic.Services
{
    /// <summary>
    /// Bet service implementation.
    /// </summary>
    /// <seealso cref="BetService.BusinessLogic.Contracts.Services.IBetService" />
    public class BetService : IBetService
    {
        private readonly IDataContext _dataContext;
        private readonly IBetRepository _betRepository;
        private readonly IBetProvider _betProvider;
        private readonly ILogger<BetService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="BetService"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="betRepository">The bet repository.</param>
        /// <param name="betProvider">The bet provider.</param>
        public BetService(IDataContext dataContext,
            IBetRepository betRepository,
            IBetProvider betProvider,
            ILogger<BetService> logger)
        {
            _dataContext = dataContext;
            _betRepository = betRepository;
            _betProvider = betProvider;
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task CompletePayoutStatues(IEnumerable<Bet> entities, CancellationToken cancellationToken)
        {
            foreach (var entity in entities)
            {
                entity.PayoutStatus = BetPayoutStatus.Paid;
            }

            await _betRepository.UpdateRange(entities, cancellationToken);
            await _dataContext.SaveChanges(cancellationToken);

            _logger.LogTrace("Payments status has been updated for 'Paid' for bets, Counts={entities.Count} ", entities.Count());
        }

        /// <inheritdoc />
        public async Task Create(Bet bet, CancellationToken cancellationToken)
        {
            await _betRepository.Add(bet, cancellationToken);
            await _dataContext.SaveChanges(cancellationToken);
        }

        /// <inheritdoc />
        public async Task CreateRange(List<Bet> bets, CancellationToken cancellationToken)
        {
            await _betRepository.AddRange(bets, cancellationToken);
            await _dataContext.SaveChanges(cancellationToken);
        }

        /// <inheritdoc />
        public Task<Bet?> GetById(Guid id, CancellationToken cancellationToken)
        {
            return _betProvider.GetBetById(id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<Bet>> GetRangeByCoefficientId(Guid coefficientId, CancellationToken cancellationToken)
        {
            var items = _betProvider.GetRangeByCoefficientId(coefficientId, cancellationToken);

            return items;
        }

        /// <inheritdoc/>
        public Task<IEnumerable<Bet>> GetRangeByCoefficientIds(IEnumerable<Guid> coefficientIds, CancellationToken cancellationToken)
        {
            return _betProvider.GetRangeByCoefficientIds(coefficientIds, cancellationToken);
        }

        /// <inheritdoc />
        public Task<List<Bet>> GetRangeByUserId(Guid userId, int page, int pageSize, CancellationToken cancellationToken)
        {
            return _betProvider.GetBetsRangeByUserId(userId, page, pageSize, cancellationToken);
        }

        /// <inheritdoc />
        public Task<IEnumerable<Bet>> GetRangeProcessingBets(CancellationToken cancellationToken)
        {
            return _betProvider.GetRangeProcessingBets(cancellationToken);
        }

        /// <inheritdoc />
        public async Task UpdateStatuses(IEnumerable<BetStatusUpdateModel> betStatusUpdateModels, CancellationToken cancellationToken)
        {
            var tasks = new Task[]
            {
                _betRepository.UpdateBetStatuseByCoefficientIds(
                betStatusUpdateModels.WithBetStatusType(BetStatusType.Lose).Select(x => x.CoefficientId),
                BetStatusType.Lose,
                cancellationToken),

                _betRepository.UpdateBetStatuseByCoefficientIds(
                betStatusUpdateModels.WithBetStatusType(BetStatusType.Blocked).Select(x => x.CoefficientId),
                BetStatusType.Blocked,
                cancellationToken),

                _betRepository.UpdateBetStatuseAndPayoutStatusByCoefficientIds(
                betStatusUpdateModels.WithBetStatusType(BetStatusType.Win).Select(x => x.CoefficientId),
                BetStatusType.Win,
                BetPayoutStatus.Processing, cancellationToken),

                _betRepository.UpdateBetStatuseAndPayoutStatusByCoefficientIds(
                betStatusUpdateModels.WithBetStatusType(BetStatusType.Canceled).Select(x => x.CoefficientId),
                BetStatusType.Canceled,
                BetPayoutStatus.Processing, cancellationToken)
            };

            await Task.WhenAll(tasks);

            await _dataContext.SaveChanges(cancellationToken);

            _logger.LogTrace("{BetStatusType} and {BetpayoutStatus} has been updated for bets, Counts={entities.Count} ", nameof(BetStatusType), nameof(BetPayoutStatus), betStatusUpdateModels.Count());
        }

        /// <summary>
        /// Update bet statuses and get collection of bets with processing status
        /// </summary>
        /// <param name="betStatusUpdateModels"></param>
        /// <param name="token"></param>
        /// <returns>IEnumerable<Bet></returns>
        public async Task<IEnumerable<Bet>> HandleUpdateStatuses(IEnumerable<BetStatusUpdateModel> betStatusUpdateModels, CancellationToken token)
        {
            await UpdateStatuses(betStatusUpdateModels, token);
            var existingProcessingBets = await GetRangeProcessingBets(token);
            return existingProcessingBets;
        }
    }
}
