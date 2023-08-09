using DRRCore.Domain.Entities.MYSQLContext;

namespace DRRCore.Domain.Interfaces
{
    public interface ITCuponDomain
    {
        Task<TCupon> GetTCuponByCodigoAsync(int codigo);
        Task<List<TCupon>> GetAllTCuponAsync();
    }
}
