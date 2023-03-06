using BetService.BusinessLogic.Models.CashServiceModels.Enums;

namespace BetService.BusinessLogic.Models.CashServiceModels.Models
{
    public class CashServiceTransaction
    {
        public Guid TransactionId { get; set; }

        public Guid TransactionProfileId { get; set; }

        public CashServiceProfile CashServiceProfile { get; set; }

        public CashServiceCashType CashServiceCashType { get; set; }

        public decimal Amount { get; set; }
    }
}
