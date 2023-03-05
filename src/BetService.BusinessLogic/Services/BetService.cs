using BetService.BusinessLogic.Contracts.DataAccess;
using BetService.BusinessLogic.Contracts.DataAccess.Providers;
using BetService.BusinessLogic.Contracts.DataAccess.Repositories;
using BetService.BusinessLogic.Contracts.Services;
using BetService.BusinessLogic.Enums;
using BetService.BusinessLogic.Models;
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
            entities.ToList().ForEach(x => x.betPaidType = BetPayoutStatus.Paid);

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
        public async Task<IEnumerable<Bet>> UpdateStatuses(IEnumerable<BetStatusUpdateModel> betStatusUpdateModels, CancellationToken cancellationToken)
        {
            var entities = await _betProvider.GetRangeByCoefficientIds(betStatusUpdateModels.Select(x => x.CoefficientId), cancellationToken);

            foreach (var betStatusUpdateModel in betStatusUpdateModels)
            {
                switch (betStatusUpdateModel.StatusType)
                {
                    case Enums.BetStatusType.Win:
                        entities.Where(x => x.CoefficientId.Equals(betStatusUpdateModel.CoefficientId))
                            .ToList()
                            .ForEach(x => { x.BetStatusType = betStatusUpdateModel.StatusType; x.betPaidType = BetPayoutStatus.Processing; });
                        break;

                    case Enums.BetStatusType.Canceled:
                        break;
                }
            }

            await _betRepository.UpdateRange(entities, cancellationToken);
            await _dataContext.SaveChanges(cancellationToken);

            _logger.LogTrace("{BetStatusType} and {BetpayoutStatus} has been updated for bets, Counts={entities.Count} ", nameof(BetStatusType), nameof(BetPayoutStatus), entities.Count());

            return entities;
        }
    }
}
