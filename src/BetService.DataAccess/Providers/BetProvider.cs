using BetService.BusinessLogic.Contracts.DataAccess.Providers;
using BetService.BusinessLogic.Entities;
using BetService.BusinessLogic.Enums;
using Microsoft.EntityFrameworkCore;

namespace BetService.DataAccess.Providers
{
    /// <summary>
    /// Bet provider implementation
    /// </summary>
    /// <seealso cref="BetService.BusinessLogic.Contracts.DataAccess.Providers.IBetProvider" />
    public class BetProvider : IBetProvider
    {
        private readonly DbSet<Bet> _entities;

        /// <summary>
        /// Initializes a new instance of the <see cref="BetProvider"/> class.
        /// </summary>
        /// <param name="entities">The entities.</param>
        public BetProvider(DbSet<Bet> entities)
        {
            _entities = entities;
        }

        /// <inheritdoc />
        public Task<Bet?> GetBetById(Guid id, CancellationToken cancellationToken)
        {
            return _entities
                .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Bet>> GetBetsRangeByUserId(Guid userId, int page, int pageSize, CancellationToken cancellationToken)
        {
            return await _entities
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreateAtUtc)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<Bet>> GetRangeByCoefficientId(Guid coefficientId, CancellationToken cancellationToken)
        {
            return await _entities
                .AsNoTracking()
                .Where(x => x.CoefficientId == coefficientId)
                .ToListAsync(cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Bet>> GetRangeByCoefficientIds(IEnumerable<Guid> coefficientIds, CancellationToken cancellationToken)
        {
            return await _entities
                .AsNoTracking()
                .Where(x => coefficientIds.Contains(x.CoefficientId))
                .ToListAsync(cancellationToken);
        }

        public async Task<IEnumerable<Bet>> GetRangeProcessingBets(CancellationToken cancellationToken)
        {
            return await _entities
                .AsNoTracking()
                .Where(x => x.PayoutStatus == BetPayoutStatus.Processing)
                .ToListAsync(cancellationToken);
        }
    }
}
