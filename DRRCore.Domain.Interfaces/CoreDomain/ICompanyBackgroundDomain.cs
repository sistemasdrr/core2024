using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Domain.Interfaces.CoreDomain
{
    public interface ICompanyBackgroundDomain : IBaseDomain<CompanyBackground>
    {
        Task<bool> UpdateAsync(CompanyBackground obj, List<Traduction> traductions);
        Task<bool> AddAsync(CompanyBackground obj, List<Traduction> traductions);
    }
}
