using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Domain.Interfaces.CoreDomain
{
    public interface ICompanySBSDomain : IBaseDomain<CompanySb>
    {
        Task<int> AddCompanySBS(CompanySb companySb);
        Task<int> UpdateCompanySBS(CompanySb companySb);
        Task<CompanySb> GetByIdCompany(int idCompany);
    }
}
