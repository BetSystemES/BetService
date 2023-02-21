using BetService.BusinessLogic.Contracts.DataAccess;

namespace BetService.DataAccess
{
    public class BetDataContext : IDataContext
    {
        private readonly BetDbContext _betDbContext;

        public BetDataContext(BetDbContext betDbContext)
        {
            _betDbContext = betDbContext;
        }

        public Task SaveChanges(CancellationToken token)
        {
            return _betDbContext.SaveChangesAsync(token);
        }
    }
}
