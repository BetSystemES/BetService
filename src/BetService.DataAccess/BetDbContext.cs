using BetService.DataAccess.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BetService.DataAccess
{
    /// <summary>
    /// Db context for Bet Database
    /// </summary>
    /// <seealso cref="DbContext" />
    public class BetDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BetDbContext"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public BetDbContext(DbContextOptions<BetDbContext> options)
            : base(options) { }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <remarks>
        /// <para>
        /// If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
        /// then this method will not be run.
        /// </para>
        /// <para>
        /// See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> for more information.
        /// </para>
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BetConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
