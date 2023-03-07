using System.Linq;
using System.Linq.Expressions;
using BetService.BusinessLogic.Entities;
using BetService.BusinessLogic.Enums;

namespace BetService.BusinessLogic.Helpers;

/// <summary>
/// Quary helpers for <seealso cref="Bet"/>
/// </summary>
public static class BetQueryHelper
{
    private static readonly IEnumerable<BetPayoutStatus> _activeStatuses = new List<BetPayoutStatus>
    {
        BetPayoutStatus.Unspecified,
        BetPayoutStatus.Processing,
    };

    /// <summary>
    /// Determines whether [contains coefficient ids].
    /// </summary>
    /// <param name="ids">The ids.</param>
    /// <returns>The delegate that represents the lambda expression.</returns>
    public static Expression<Func<Bet, bool>> ContainsCoefficientIds(IEnumerable<Guid> ids)
    {
        return x =>
            ids.Contains(x.CoefficientId);
    }

    /// <summary>
    /// Determines whether [contains coefficient ids] and [conatins active <seealso cref="BetStatusType"/>].
    /// </summary>
    /// <param name="ids">The ids.</param>
    /// <returns>The delegate that represents the lambda expression.</returns>
    public static Expression<Func<Bet, bool>> ActiveAndContainsCoefficientids(IEnumerable<Guid> ids)
    {
        return x =>
            ids.Contains(x.CoefficientId) &&
            _activeStatuses.Contains(x.PayoutStatus);
    }
}
