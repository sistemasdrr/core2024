using DRRCore.Domain.Entities.MYSQLContext;

namespace DRRCore.Domain.Interfaces
{
    public interface ITTemporalPersonaDomain
    {
        Task<TTemporalPersona> GetTemporalPersonaByCodigoAsync(string codigo);
        Task<List<TTemporalPersona>> GetAllTemporalPersonaAsync();
    }
}
