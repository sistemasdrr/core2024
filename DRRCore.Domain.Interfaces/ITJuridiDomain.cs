using DRRCore.Domain.Entities.MYSQLContext;

namespace DRRCore.Domain.Interfaces
{
    public interface ITJuridiDomain
    {
        Task<List<TJuridi>> GetAllTJuridiAsync();
        Task<TJuridi> GetTJuridiByCodigoAsync(string codigo);
    }
}
