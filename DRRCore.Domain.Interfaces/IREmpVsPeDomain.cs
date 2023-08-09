using DRRCore.Domain.Entities.MYSQLContext;

namespace DRRCore.Domain.Interfaces
{
    public interface IREmpVsPeDomain
    {
        Task<REmpVsPe> GetREmpVsPeByCodigoAsync(string codigo);
        Task<List<REmpVsPe>> GetAllREmpVsPeAsync();
    }
}
