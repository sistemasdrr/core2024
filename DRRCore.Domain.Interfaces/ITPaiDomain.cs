using DRRCore.Domain.Entities.MYSQLContext;

namespace DRRCore.Domain.Interfaces
{
    public interface ITPaiDomain
    {
        Task<List<TPai>> GetAllTPaisAsync();
        Task<TPai> GetTPaiByCodigoAsync(string codigo);
    }
}
