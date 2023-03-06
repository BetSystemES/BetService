namespace BetService.BusinessLogic.Models.CashServiceModels.Models
{
    public class CashServiceProfile
    {
        public Guid ProfileId { get; set; }

        public List<CashServiceTransaction> Transactions { get; set; }
    }
}
