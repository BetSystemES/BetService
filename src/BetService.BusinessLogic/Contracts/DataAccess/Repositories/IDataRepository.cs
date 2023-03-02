namespace BetService.BusinessLogic.Contracts.DataAccess.Repositories
{
    /// <summary>
    /// The data repository contract.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IDataRepository<in TEntity>
    {
        /// <summary>
        /// Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task</returns>
        Task Add(TEntity entity, CancellationToken token);

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task</returns>
        Task AddRange(IEnumerable<TEntity> entities, CancellationToken token);

        /// <summary>
        /// Removes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task</returns>
        Task Remove(TEntity entity, CancellationToken token);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task</returns>
        Task Update(TEntity entity, CancellationToken token);

        /// <summary>
        /// Updates the range.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="token">The token.</param>
        /// <returns>Task</returns>
        Task UpdateRange(IEnumerable<TEntity> entities, CancellationToken token);
    }
}
