using BetService.BusinessLogic.Entities;
using BetService.BusinessLogic.Enums;

namespace BetService.BusinessLogic.Extensions
{
    /// <summary>
    /// Extenions for <seealso cref="BetStatusUpdateModel"/>
    /// </summary>
    public static class BetStatusUpdateModelExtension
    {
        /// <summary>
        /// Withes the type of the bet status.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="betStatusType">Type of the bet status.</param>
        /// <returns>IEnumerable<BetStatusUpdateModel></returns>
        public static IEnumerable<BetStatusUpdateModel> WithBetStatusType(this IEnumerable<BetStatusUpdateModel> items, BetStatusType betStatusType)
        {
            return items.Where(x => x.StatusType.Equals(betStatusType));
        }
    }
}
