using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Domain.Interfaces.CoreDomain
{
    public interface ICompanyDomain:IBaseDomain<Company>
    {
        Task<List<Company>> GetByNameAsync(string name, string form, int idCountry, bool haveReport);
        Task<bool> ActiveWebVision(int id);
        Task<bool> DesactiveWebVision(int id);
        Task<int> AddCompanyAsync(Company obj);
        Task<Company> GetByOldCode(string oldCode);
    }
}
