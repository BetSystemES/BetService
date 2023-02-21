using BetService.BusinessLogic.Contracts.DataAccess;
using BetService.BusinessLogic.Contracts.DataAccess.Providers;
using BetService.BusinessLogic.Contracts.DataAccess.Repositories;
using BetService.BusinessLogic.Contracts.Services;
using BetService.BusinessLogic.Models;

namespace BetService.BusinessLogic.Services
{
    public class BetService : IBetService
    {
        private readonly IDataContext _dataContext;
        private readonly IBetRepository _betRepository;
        private readonly IBetProvider _betProvider;

        public BetService(IDataContext dataContext,
            IBetRepository betRepository,
            IBetProvider betProvider)
        {
            _dataContext = dataContext;
            _betRepository = betRepository;
            _betProvider = betProvider;
        }

        public async Task Create(Bet bet, CancellationToken cancellationToken)
        {
            await _betRepository.Add(bet, cancellationToken);
            await _dataContext.SaveChanges(cancellationToken);
        }

        public async Task CreateRange(List<Bet> bets, CancellationToken cancellationToken)
        {
            await _betRepository.AddRange(bets, cancellationToken);
            await _dataContext.SaveChanges(cancellationToken);
        }

        public async Task<Bet?> GetById(Guid id, CancellationToken cancellationToken)
        {
            var item = await _betProvider.GetBetById(id, cancellationToken);

            return item;
        }

        public async Task<List<Bet>> GetRangeByUserId(Guid userId, int page, int pageSize, CancellationToken cancellationToken)
        {
            var items = await _betProvider.GetBetsRangeByUserId(userId, page, pageSize, cancellationToken);

            return items;
        }

        public Task UpdateStatuses(List<BetStatusUpdateModel> betStatusUpdateModels, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
