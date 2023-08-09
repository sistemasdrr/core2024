using DRRCore.Domain.Entities.MYSQLContext;

namespace DRRCore.Domain.Interfaces
{
    public interface IREmpVsRamNegDomain
    {
        Task<REmpVsRamNeg> GetREmpVsRamNegByCodigoAsync(string codigo);
        Task<List<REmpVsRamNeg>> GetAllREmpVsRamNegAsync();
    }
}
