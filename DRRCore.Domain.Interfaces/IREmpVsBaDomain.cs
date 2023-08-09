using DRRCore.Domain.Entities.MYSQLContext;

namespace DRRCore.Domain.Interfaces
{
    public interface IREmpVsBaDomain
    {
        Task<REmpVsBa> GetREmpVsBaByCodigoAsync(string codigo);
        Task<List<REmpVsBa>> GetAllREmpVsBaAsync();
    }
}
