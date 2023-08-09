using DRRCore.Domain.Entities.MYSQLContext;

namespace DRRCore.Domain.Interfaces
{
    public interface ITCalidadDomain
    {
        Task<TCalidad> GetTCalidadByCodigoAsync(string codigo);
        Task<List<TCalidad>> GetAllTCalidadAsync();
    }
}
