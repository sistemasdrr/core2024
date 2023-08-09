using DRRCore.Domain.Entities.MYSQLContext;

namespace DRRCore.Domain.Interfaces
{
    public interface ITSectorDomain
    {
        Task<TSector> GetTSectorByCodigoAsync(string codigo);
        Task<List<TSector>> GetAllTSectorAsync();
    }
}
