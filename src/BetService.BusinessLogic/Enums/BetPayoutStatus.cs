﻿namespace BetService.BusinessLogic.Enums
{
    /// <summary>
    /// Bet payout status.
    /// </summary>
    public enum BetPayoutStatus
    {
        /// <summary>
        /// The unspecified
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// The processing
        /// </summary>
        Processing = 1,

        /// <summary>
        /// The paid
        /// </summary>
        Paid = 2,

        /// <summary>
        /// The blocked
        /// </summary>
        Blocked = 3,
    }
}
