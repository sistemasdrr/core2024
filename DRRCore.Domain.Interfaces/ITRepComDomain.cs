using DRRCore.Domain.Entities.MYSQLContext;

namespace DRRCore.Domain.Interfaces
{
    public interface ITRepComDomain
    {
        Task<TRepCom> GetTRepComByCodigoAsync(string codigo);
        Task<List<TRepCom>> GetAllTRepComAsync();
    }
}
