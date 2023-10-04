using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Infraestructure.Interfaces.CoreRepository
{
    public interface IPermissionRepository : IBaseRepository<Permission>
    {
        Task<List<Permission>> GetByRol(int idRol);
    }
}
