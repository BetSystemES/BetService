﻿using BetService.BusinessLogic.Contracts.DataAccess.Providers;
using BetService.BusinessLogic.Models;
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
