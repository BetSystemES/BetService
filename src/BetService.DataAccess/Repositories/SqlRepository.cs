using Microsoft.EntityFrameworkCore;

namespace BetService.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of sql repository.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class SqlRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> _entities;

        private readonly bool _useHiLoGenerators;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="useHiLoGenerators">if set to <c>true</c> [use hi lo generators].</param>
        protected SqlRepository(DbSet<TEntity> entities, bool useHiLoGenerators = false)
        {
            _entities = entities;
            _useHiLoGenerators = useHiLoGenerators;
        }

        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="token">The token.</param>
        /// <exception cref="System.ArgumentNullException">entity</exception>
        public virtual async Task Add(TEntity entity, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(entity, "entity");
            if (_useHiLoGenerators)
            {
                await _entities.AddAsync(entity, token);
            }
            else
            {
                _entities.Add(entity);
            }
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task.CompletedTask</returns>
        /// <exception cref="System.ArgumentNullException">entities</exception>
        public virtual Task AddRange(IEnumerable<TEntity> entities, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(entities, "entities");
            var entities2 = entities as List<TEntity> ?? entities.ToList();
            if (_useHiLoGenerators)
            {
                return _entities.AddRangeAsync(entities2, token);
            }

            _entities.AddRange(entities2);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task.CompletedTask</returns>
        /// <exception cref="System.ArgumentNullException">entity</exception>
        public virtual Task Remove(TEntity entity, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(entity, "entity");
            _entities.Remove(entity);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task.CompletedTask</returns>
        /// <exception cref="System.ArgumentNullException">entity</exception>
        public virtual Task Update(TEntity entity, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(entity, "entity");
            _entities.Update(entity);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Updates the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task.CompletedTask</returns>
        /// <exception cref="System.ArgumentNullException">entities</exception>
        public virtual Task UpdateRange(IEnumerable<TEntity> entities, CancellationToken token)
        {
            ArgumentNullException.ThrowIfNull(entities, "entities");
            _entities.UpdateRange(entities);
            return Task.CompletedTask;
        }
    }
}
