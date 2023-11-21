using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Infraestructure.Interfaces.CoreRepository
{
    public interface ICompanyBranchRepository:IBaseRepository<CompanyBranch>
    {
        Task<bool> UpdateAsync(CompanyBranch obj, List<Traduction> traductions);
        Task<bool> AddAsync(CompanyBranch obj, List<Traduction> traductions);
    }
}
