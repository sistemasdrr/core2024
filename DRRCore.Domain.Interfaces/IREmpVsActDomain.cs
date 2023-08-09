using DRRCore.Domain.Entities.MYSQLContext;

namespace DRRCore.Domain.Interfaces
{
    public  interface IREmpVsActDomain
    {
        Task<REmpVsAct> GetREmpVsActByCodigoAsync(string codigo);
        Task<List<REmpVsAct>> GetAllREmpVsActAsync();
    }
}
