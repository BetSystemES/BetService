using BetService.BusinessLogic.Contracts.DataAccess;
using BetService.BusinessLogic.Contracts.DataAccess.Providers;
using BetService.BusinessLogic.Contracts.DataAccess.Repositories;
using BetService.BusinessLogic.Contracts.Services;
using BetService.BusinessLogic.Models;

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

        /// <summary>
        /// Initializes a new instance of the <see cref="BetService"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="betRepository">The bet repository.</param>
        /// <param name="betProvider">The bet provider.</param>
        public BetService(IDataContext dataContext,
            IBetRepository betRepository,
            IBetProvider betProvider)
        {
            _dataContext = dataContext;
            _betRepository = betRepository;
            _betProvider = betProvider;
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
        public async Task<Bet?> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await _betProvider.GetBetById(id, cancellationToken);

            return item;
        }

        /// <inheritdoc />
        public async Task<List<Bet>> GetRangeByUserId(Guid userId, int page, int pageSize, CancellationToken cancellationToken)
        {
            var items = await _betProvider.GetBetsRangeByUserId(userId, page, pageSize, cancellationToken);

            return items;
        }

        /// <inheritdoc />
        public async Task UpdateStatuses(List<BetStatusUpdateModel> betStatusUpdateModels, CancellationToken cancellationToken)
        {
            foreach (var betStatusUpdateModel in betStatusUpdateModels)
            {
                await _betRepository.UpdateBetStatusByCoefficientId(
                    betStatusUpdateModel.CoefficientId,
                    betStatusUpdateModel.BetStatusType,
                    cancellationToken);
            }

            // todo: process for payout logic
            await _dataContext.SaveChanges(cancellationToken);
        }
    }
}
