using BetService.BusinessLogic.Contracts.Providers;

namespace BetService.BusinessLogic
{
    /// <summary>
    /// Date time provider implementation
    /// </summary>
    /// <seealso cref="IDateTimeProvider" />
    public class DateTimeProvider : IDateTimeProvider
    {
        /// <inheritdoc />
        public DateTime Now => DateTime.Now;

        /// <inheritdoc />
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
