using BetService.BusinessLogic.Contracts.DataAccess;
using BetService.BusinessLogic.Contracts.DataAccess.Providers;
using BetService.BusinessLogic.Contracts.DataAccess.Repositories;
using BetService.BusinessLogic.Models;
using BetService.DataAccess.Providers;
using BetService.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BetService.DataAccess
{
    public static class DataAccessServicesExtensions
    {
        /// <summary>
        /// Adds the providers.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddProviders(this IServiceCollection services)
        {
            services.AddScoped<IBetProvider, BetProvider>();

            return services;
        }

        /// <summary>
        /// Adds the repositories.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBetRepository, BetRepository>();

            return services;
        }

        /// <summary>
        /// Adds the postgres context.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="options">The options.</param>
        /// <returns>IServiceCollection</returns>
        public static IServiceCollection AddPostgresContext(this IServiceCollection services,
            Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContextPool<BetDbContext>(options);

            services.AddScoped<IDataContext, BetDataContext>();
            services.AddTransient<DbContext>(serviceProvider => serviceProvider.GetRequiredService<BetDbContext>())
                    .AddScopedDbSet<Bet>();

            return services;
        }

        /// <summary>
        /// Adds the scoped database set.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="services">The services.</param>
        /// <returns>IServiceCollection</returns>
        private static IServiceCollection AddScopedDbSet<TEntity>(this IServiceCollection services)
            where TEntity : class
        {
            services.AddScoped<DbSet<TEntity>>(serviceProvider =>
                serviceProvider.GetRequiredService<BetDbContext>().Set<TEntity>());

            return services;
        }
    }
}
