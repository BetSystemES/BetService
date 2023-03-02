using BetService.BusinessLogic.Enums;
using BetService.BusinessLogic.Models;

namespace BetService.BusinessLogic.Extensions
{
    /// <summary>
    /// Provide extensions methods for different structures with <seealso cref="Bet"/> entity.
    /// </summary>
    public static class BetStatusUpdateModelExtensions
    {
        /// <summary>
        /// Converts to positivebets.
        /// </summary>
        /// <param name="bets">The bets.</param>
        /// <returns>The list of positive bets.</returns>
        public static IEnumerable<BetStatusUpdateModel> ToPositiveBets(this IEnumerable<BetStatusUpdateModel> bets)
        {
            return bets.Where(x => x.StatusType == BetStatusType.Win);
        }
    }
}
