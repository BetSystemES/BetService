using BetService.Grpc.Infrastructure.Clients.CashService.Enums;

namespace BetService.Grpc.Infrastructure.Clients.CashService.Models
{
    public class TransactionEntity
    {
        public Guid TransactionId { get; set; }

        public Guid TransactionProfileId { get; set; }

        public TransactionProfileEntity TransactionProfileEntity { get; set; }

        public CashType CashType { get; set; }

        public decimal Amount { get; set; }
    }
}
