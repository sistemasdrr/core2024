using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Domain.Interfaces.CoreDomain
{
    public interface IProviderDomain : IBaseDomain<Provider>
    {
        Task<List<Provider>> GetProvidersByIdCompany(int idCompany);
        Task<List<Provider>> GetProviderByIdPerson(int idPerson);
    }
}
