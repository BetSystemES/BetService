using BetService.BusinessLogic.Enums;

namespace BetService.BusinessLogic.Entities
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
        public BetStatusType StatusType { get; set; }
    }
}
