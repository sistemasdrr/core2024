using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Infraestructure.Interfaces.CoreRepository
{
    public interface IProviderRepository : IBaseRepository<Provider>
    {
        Task<List<Provider>> GetProviderByIdCompany(int idCompany);
    }
}
