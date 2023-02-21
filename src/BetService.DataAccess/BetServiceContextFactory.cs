using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BetService.DataAccess
{
    /// <summary>
    /// Auction service context factory
    /// </summary>
    public class BetServiceContextFactory : IDesignTimeDbContextFactory<BetDbContext>
    {
        /// <summary>
        /// Creates a new instance of a derived context.
        /// </summary>
        /// <param name="args">Arguments provided by the design-time service.</param>
        /// <returns>
        /// An instance of <typeparamref name="TContext" />.
        /// </returns>
        public BetDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BetDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=BetDb;User Id=postgres;Password=123");

            return new BetDbContext(optionsBuilder.Options);
        }
    }
}
