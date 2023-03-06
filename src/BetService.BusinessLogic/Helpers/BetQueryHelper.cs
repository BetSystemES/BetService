using System.Linq.Expressions;
using BetService.BusinessLogic.Entities;
using BetService.BusinessLogic.Enums;

namespace BetService.BusinessLogic.Helpers;

public static class BetQueryHelper
{
    public static Expression<Func<Bet, bool>> Test1(IEnumerable<Guid> ids)
    {
        return x =>
            ids.Contains(x.CoefficientId) &&
            x.PayoutStatus != BetPayoutStatus.Paid;
    }
}
