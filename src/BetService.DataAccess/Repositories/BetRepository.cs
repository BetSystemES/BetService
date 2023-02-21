using System.Runtime.CompilerServices;
using BetService.BusinessLogic.Contracts.DataAccess.Repositories;
using BetService.BusinessLogic.Contracts.Providers;
using BetService.BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;

namespace BetService.DataAccess.Repositories
{
    /// <summary>
    /// Bet repository implementation.
    /// </summary>
    public class BetRepository : SqlRepository<Bet>, IBetRepository
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public BetRepository(DbSet<Bet> provider, IDateTimeProvider dateTimeProvider) : base(provider, true)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public override Task Add(Bet entity, CancellationToken token)
        {
            entity.CreateAtUtc = _dateTimeProvider.NowUtc;
            return base.Add(entity, token);
        }

        public override Task AddRange(IEnumerable<Bet> entities, CancellationToken token)
        {
            entities.ToList().ForEach(x => x.CreateAtUtc = _dateTimeProvider.NowUtc);

            return base.AddRange(entities, token);
        }
    }
}
