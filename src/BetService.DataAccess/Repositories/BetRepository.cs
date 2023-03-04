using BetService.BusinessLogic.Contracts.DataAccess.Repositories;
using BetService.BusinessLogic.Contracts.Providers;
using BetService.BusinessLogic.Enums;
using BetService.BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;

namespace BetService.DataAccess.Repositories
{
    /// <summary>
    /// Bet repository implementation.
    /// </summary>
    public class BetRepository : SqlRepository<Bet>, IBetRepository
    {
        private readonly DbSet<Bet> _entities;
        private readonly IDateTimeProvider _dateTimeProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="BetRepository"/> class.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="dateTimeProvider">The date time provider.</param>
        public BetRepository(DbSet<Bet> provider, IDateTimeProvider dateTimeProvider) : base(provider, true)
        {
            _entities = provider;
            _dateTimeProvider = dateTimeProvider;
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task</returns>
        public override Task Add(Bet entity, CancellationToken token)
        {
            entity.CreateAtUtc = _dateTimeProvider.NowUtc;

            return base.Add(entity, token);
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task</returns>
        public override Task AddRange(IEnumerable<Bet> entities, CancellationToken token)
        {
            var now = _dateTimeProvider.NowUtc;
            entities.ToList().ForEach(x => x.CreateAtUtc = now);

            return base.AddRange(entities, token);
        }
    }
}
