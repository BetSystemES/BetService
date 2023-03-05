namespace BetService.Grpc.Infrastructure.Clients.CashService.Models
{
    public class TransactionProfileEntity
    {
        public Guid ProfileId { get; set; }

        public List<TransactionEntity> Transactions { get; set; }
    }
}
