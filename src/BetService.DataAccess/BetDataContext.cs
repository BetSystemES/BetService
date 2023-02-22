using BetService.BusinessLogic.Contracts.DataAccess;

namespace BetService.DataAccess
{
    /// <summary>
    /// Implementation of data context for bet databse.
    /// </summary>
    /// <seealso cref="BetService.BusinessLogic.Contracts.DataAccess.IDataContext" />
    public class BetDataContext : IDataContext
    {
        private readonly BetDbContext _betDbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BetDataContext"/> class.
        /// </summary>
        /// <param name="betDbContext">The bet database context.</param>
        public BetDataContext(BetDbContext betDbContext)
        {
            _betDbContext = betDbContext;
        }

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>
        /// Task
        /// </returns>
        public Task SaveChanges(CancellationToken token)
        {
            return _betDbContext.SaveChangesAsync(token);
        }
    }
}
