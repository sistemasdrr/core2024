using DRRCore.Domain.Entities.MYSQLContext;

namespace DRRCore.Domain.Interfaces
{
    public interface ITTipoTramiteDomain
    {
        Task<TTipoTramite> GetTTipoTramiteByCodigoAsync(string codigo);
        Task<List<TTipoTramite>> GetAllTTipoTramiteAsync();
    }
}
