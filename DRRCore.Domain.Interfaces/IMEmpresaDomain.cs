using DRRCore.Domain.Entities.MYSQLContext;

namespace DRRCore.Domain.Interfaces
{
    public interface IMEmpresaDomain
    {
        Task<MEmpresa> GetmEmpresaByCodigoAsync(string codigo);
    }
}
