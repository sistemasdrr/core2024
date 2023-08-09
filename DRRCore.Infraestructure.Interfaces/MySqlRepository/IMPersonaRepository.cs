using DRRCore.Domain.Entities.MYSQLContext;

namespace DRRCore.Infraestructure.Interfaces.MySqlRepository
{
    public interface IMPersonaRepository
    {//Usp_TraducirPeComRep (TRADUCIR COMENTARIO DE REPUTACION)
        Task<MPersona> GetMPersonaByCodigoAsync(string codigo);
        Task<List<MPersona>> GetAllMPersonaAsync();
    }
}
