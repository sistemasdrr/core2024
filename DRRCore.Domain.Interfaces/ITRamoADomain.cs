using DRRCore.Domain.Entities.MYSQLContext;

namespace DRRCore.Domain.Interfaces
{
    public interface ITRamoADomain
    {
        Task<TRamoA> GetTRamoAByCodigoAsync(string codigo);
        Task<List<TRamoA>> GetAllTRamoAAsync();
    }
}
