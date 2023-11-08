using DRRCore.Domain.Entities.MYSQLContext;

namespace DRRCore.Domain.Interfaces.MysqlDomain
{
    public interface ITCuponDomain
    {
        Task<TCupon> GetTCuponByCodigoAsync(int codigo);
        Task<List<TCupon>> GetAllTCuponAsync();
    }
}
