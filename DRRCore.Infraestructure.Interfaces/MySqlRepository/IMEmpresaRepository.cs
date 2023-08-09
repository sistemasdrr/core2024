using DRRCore.Domain.Entities.MYSQLContext;

namespace DRRCore.Infraestructure.Interfaces.MySqlRepository
{
    public interface IMEmpresaRepository
    {
        Task<MEmpresa> GetmEmpresaByCodigoAsync(string codigo);
    }
}
