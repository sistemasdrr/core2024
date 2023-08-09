using DRRCore.Domain.Entities.MYSQLContext;

namespace DRRCore.Domain.Interfaces
{
    public interface IREmpVsInfFinDomain
    {
        Task<REmpVsInfFin> GetREmpVsInfByCodigoAsync(string codigo);
        Task<List<REmpVsInfFin>> GetAllREmpVsInfAsync();
    }
}
