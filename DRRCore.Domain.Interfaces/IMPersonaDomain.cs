using DRRCore.Domain.Entities.MYSQLContext;
namespace DRRCore.Domain.Interfaces
{
    public interface IMPersonaDomain
    {
        Task<MPersona> GetMPersonaByCodigoAsync(string codigo);
        Task<List<MPersona>> GetAllMPersonaAsync();
    }
}
