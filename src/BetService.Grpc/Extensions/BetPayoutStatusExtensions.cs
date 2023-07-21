namespace BetService.Grpc.Extensions
{
    public static class BetPayoutStatusExtensions
    {
        public static List<BetStatusType> _paybleStatuses = new List<BetStatusType>()
        {
            BetStatusType.Win,
            BetStatusType.Canceled
        };

        public static bool IsPayable(this BetStatusType payoutStatus)
        {
            return _paybleStatuses.Contains(payoutStatus);
        }
    }
}
