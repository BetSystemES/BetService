using BetService.BusinessLogic.Contracts.Providers;

namespace BetService.BusinessLogic.Providers
{
    /// <summary>
    /// Date time provider implementation
    /// </summary>
    /// <seealso cref="IDateTimeProvider"/>
    public class DateTimeProvider : IDateTimeProvider
    {
        /// <inheritdoc />
        public DateTime Now => DateTime.Now;

        /// <inheritdoc />
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
