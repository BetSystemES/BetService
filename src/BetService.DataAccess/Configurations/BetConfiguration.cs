using BetService.BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BetService.DataAccess.Configurations
{
    /// <summary>
    /// Configuration for bet model entity.
    /// </summary>
    public class BetConfiguration : IEntityTypeConfiguration<Bet>
    {
        /// <summary>
        /// Configures the entity of type <typeparamref name="TEntity" />.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity type.</param>
        public void Configure(EntityTypeBuilder<Bet> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.UserId).IsRequired();
            builder.Property(x => x.CoefficientId).IsRequired();
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Rate).IsRequired();
            builder.Property(x => x.betPaidType).IsRequired();
            builder.Property(x => x.BetStatusType).IsRequired();
            builder.Property(x => x.CreateAtUtc).IsRequired();

            builder.ToTable("Bets");
        }
    }
}
