using BetService.BusinessLogic.Enums;
using BetService.BusinessLogic.Models;

namespace BetService.BusinessLogic.Extensions
{
    /// <summary>
    /// Provide extensions methods for different structures with <seealso cref="Bet"/> entity.
    /// </summary>
    public static class BetExtension
    {
        /// <summary>
        /// Converts to bets with <seealso cref="BetStatusType.Win"/> and with <seealso cref="BetPayoutStatus.Processing"/>.
        /// </summary>
        /// <param name="bets">The bets.</param>
        /// <returns>The list of positive unpaid bets.</returns>
        public static IEnumerable<Bet> ToPositiveProcessingBets(this IEnumerable<Bet> bets)
        {
            return bets.Where(x => x.BetStatusType == BetStatusType.Win && x.betPaidType == BetPayoutStatus.Processing);
        }
    }
}
