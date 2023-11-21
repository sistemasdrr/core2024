using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Infraestructure.Interfaces.CoreRepository
{
    public interface ICompanyBackgroundRepository:IBaseRepository<CompanyBackground>
    {
        Task<bool> UpdateAsync(CompanyBackground obj, List<Traduction> traductions);
        Task<bool> AddAsync(CompanyBackground obj, List<Traduction> traductions);
    }
}
