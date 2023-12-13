using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Infraestructure.Interfaces.CoreRepository
{
    public interface ICompanyFinancialInformationRepository : IBaseRepository<CompanyFinancialInformation>
    {
        Task<bool> AddCompanyFinancialInformation (CompanyFinancialInformation obj, List<Traduction> traduction);
        Task<bool> UpdateCompanyFinancialInformation(CompanyFinancialInformation obj, List<Traduction> traduction);
        Task<CompanyFinancialInformation> GetByIdCompany(int idCompany);
    }
}
