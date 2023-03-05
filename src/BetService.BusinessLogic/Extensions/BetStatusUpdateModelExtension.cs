using BetService.BusinessLogic.Entities;
using BetService.BusinessLogic.Enums;

namespace BetService.BusinessLogic.Extensions
{
    public static class BetStatusUpdateModelExtension
    {
        public static IEnumerable<BetStatusType> PayableStatuses
        {
            get
            {
                yield return BetStatusType.Win;
                yield return BetStatusType.Canceled;
            }
        }

        public static IEnumerable<BetStatusUpdateModel> ToPayable(this IEnumerable<BetStatusUpdateModel> items)
        {
            return items.Where(x => PayableStatuses.Contains(x.StatusType));
        }

        public static IEnumerable<BetStatusUpdateModel> WithBetStatusType(this IEnumerable<BetStatusUpdateModel> items, BetStatusType betStatusType)
        {
            return items.Where(x => x.StatusType.Equals(betStatusType));
        }
    }
}
