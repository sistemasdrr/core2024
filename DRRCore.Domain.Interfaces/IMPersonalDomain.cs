using DRRCore.Domain.Entities.MYSQLContext;

namespace DRRCore.Domain.Interfaces
{
    public interface IMPersonalDomain
    {
        Task<List<MPersonal>> GetAllActivePersonal();
    }
}
