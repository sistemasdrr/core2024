using DRRCore.Domain.Entities.MYSQLContext;

namespace DRRCore.Domain.Interfaces
{
    public interface ITContinenteDomain
    {
        Task<TContinente> GetTContinenteByCodigoAsync(string codigo);
        Task<List<TContinente>> GetAllTContinenteAsync();
    }
}
