using BetService.BusinessLogic.Enums;

namespace BetService.BusinessLogic.Models
{
    /// <summary>
    /// Bet status update model.
    /// </summary>
    public class BetStatusUpdateModel
    {
        /// <summary>
        /// Coefficient identifier.
        /// </summary>
        public Guid CoefficientId { get; set; }

        /// <summary>
        /// Bet status type.
        /// </summary>
        public BetStatusType BetStatusType { get; set; }
    }
}
