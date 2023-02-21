using BetService.BusinessLogic.Contracts.DataAccess.Providers;
using BetService.BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;

namespace BetService.DataAccess.Providers
{
    public class BetProvider : IBetProvider
    {
        private readonly DbSet<Bet> _entities;

        public BetProvider(DbSet<Bet> entities)
        {
            _entities = entities;
        }

        public Task<Bet?> GetBetById(Guid id, CancellationToken cancellationToken)
        {
            return _entities
                .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public Task<List<Bet>> GetBetsRangeByUserId(Guid userId, int page, int pageSize, CancellationToken cancellationToken)
        {
            var result = _entities
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return result;
        }
    }
}
