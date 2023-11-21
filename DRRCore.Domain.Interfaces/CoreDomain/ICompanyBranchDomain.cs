using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Domain.Interfaces.CoreDomain
{
    public interface ICompanyBranchDomain:IBaseDomain<CompanyBranch>
    {
        Task<bool> UpdateAsync(CompanyBranch obj, List<Traduction> traductions);
        Task<bool> AddAsync(CompanyBranch obj, List<Traduction> traductions);
    }
}
