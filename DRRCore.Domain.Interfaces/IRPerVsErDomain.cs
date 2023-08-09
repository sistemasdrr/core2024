using DRRCore.Domain.Entities.MYSQLContext;

namespace DRRCore.Domain.Interfaces
{
    public interface IRPerVsErDomain
    {
        Task<RPerVsEr> GetRPerVsErByCodigoAsync(string codigo);
        Task<List<RPerVsEr>> GetAllRPerVsErAsync();
    }
}
