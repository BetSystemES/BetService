using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetService.BusinessLogic.Enums
{
    /// <summary>
    /// Bet paid type.
    /// </summary>
    public enum BetPaidType
    {
        Unspecified = 0,

        Processing = 1,

        Paid = 2,

        Blocked = 3,
    }
}
