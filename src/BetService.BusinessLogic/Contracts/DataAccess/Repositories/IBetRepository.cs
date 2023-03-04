using BetService.BusinessLogic.Enums;
using BetService.BusinessLogic.Models;

namespace BetService.BusinessLogic.Contracts.DataAccess.Repositories
{
    /// <summary>
    /// Bet repository contract.
    /// </summary>
    public interface IBetRepository : IDataRepository<Bet>
    {
    }
}
