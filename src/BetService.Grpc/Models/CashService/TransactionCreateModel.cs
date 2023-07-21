using BetService.Grpc.Models.CashService.Enums;

namespace BetService.Grpc.Models.CashService
{
    public class TransactionCreateModel
    {
        public CashType CashType { get; set; }
        public double Amount { get; set; }
    }
}
