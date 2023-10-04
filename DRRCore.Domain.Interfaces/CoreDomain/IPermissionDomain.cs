using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Domain.Interfaces.CoreDomain
{
    public interface IPermissionDomain : IBaseDomain<Permission>
    {
        Task<List<Permission>> GetByRol(int idRol);
    }
}
