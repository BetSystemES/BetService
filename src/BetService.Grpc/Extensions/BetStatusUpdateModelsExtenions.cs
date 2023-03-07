using CashService.GRPC;
using BusinessEnums = BetService.BusinessLogic.Enums;
using BusinessModels = BetService.BusinessLogic.Entities;

namespace BetService.Grpc.Extensions
{
    /// <summary>
    /// Extenions for <seealso cref="BusinessModels.Bet"/>
    /// </summary>
    public static class BetExtenions
    {
        /// <summary>
        /// Converts to <seealso cref="DepositRangeRequest"/>.
        /// </summary>
        /// <param name="bets">The bets.</param>
        /// <returns><seealso cref="DepositRangeRequest"/></returns>
        public static DepositRangeRequest ToDepositRangeRequest(this IEnumerable<BusinessModels.Bet> bets)
        {
            var request = new DepositRangeRequest();

            foreach (var bet in bets)
            {
                var transactionAmount = bet.BetStatusType switch
                {
                    BusinessEnums.BetStatusType.Win => bet.Amount * bet.Rate,
                    BusinessEnums.BetStatusType.Canceled => bet.Amount,
                    _ => throw new ArgumentException($"Incorrect {nameof(BetStatusType)} with {nameof(BetPayoutStatus.Processing)} for bet with id={bet.Id} ")
                };

                var transactionModel = new TransactionModel
                {
                    ProfileId = bet.UserId.ToString()
                };
                transactionModel.Transactions.Add(new Transaction()
                {
                    Amount = transactionAmount,
                    CashType = CashType.Cash,
                    // TODO: delete generation of transactionId
                    TransactionId = Guid.NewGuid().ToString(),
                });

                request.DepositRangeRequests.Add(transactionModel);
            }

            return request;
        }
    }
}
