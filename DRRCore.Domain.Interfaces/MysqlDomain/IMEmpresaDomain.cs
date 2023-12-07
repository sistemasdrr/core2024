using DRRCore.Domain.Entities.MYSQLContext;

namespace DRRCore.Domain.Interfaces.MysqlDomain
{
    public interface IMEmpresaDomain
    {
        Task<MEmpresa> GetmEmpresaByCodigoAsync(string codigo);
        Task<List<MEmpresa>> GetNotMigratedEmpresa();
        Task<bool> MigrateEmpresa(string code);
    }
}
