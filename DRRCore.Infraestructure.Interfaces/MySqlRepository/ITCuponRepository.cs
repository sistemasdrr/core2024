using DRRCore.Domain.Entities.MYSQLContext;

namespace DRRCore.Infraestructure.Interfaces.MySqlRepository
{
    public interface ITCuponRepository
    {
        Task<TCupon> GetTCuponByCodigoAsync(int codigo);
        Task<List<TCupon>> GetAllTCuponAsync();
    }
}
