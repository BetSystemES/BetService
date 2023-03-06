using BetService.BusinessLogic.Entities;
using BetService.BusinessLogic.Enums;

namespace BetService.BusinessLogic.Extensions
{
    public static class BetStatusUpdateModelExtension
    {
        public static readonly IEnumerable<BetStatusType> PayableStatuses = new List<BetStatusType>
        {
            BetStatusType.Win,
            BetStatusType.Canceled
        };

        public static IEnumerable<BetStatusUpdateModel> ToPayable(this IEnumerable<BetStatusUpdateModel> items)
        {
            // TODO: wrong
            return items.Where(x => PayableStatuses.Contains(x.StatusType));
        }

        public static IEnumerable<BetStatusUpdateModel> WithBetStatusType(this IEnumerable<BetStatusUpdateModel> items, BetStatusType betStatusType)
        {
            return items.Where(x => x.StatusType.Equals(betStatusType));
        }
    }
}
