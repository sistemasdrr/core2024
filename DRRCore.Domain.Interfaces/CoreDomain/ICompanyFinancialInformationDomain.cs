using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Domain.Interfaces.CoreDomain
{
    public interface ICompanyFinancialInformationDomain : IBaseDomain<CompanyFinancialInformation>
    {
        Task<CompanyFinancialInformation> GetByIdCompany(int idCompany);
        Task<bool> AddCompanyFinancialInformation(CompanyFinancialInformation obj, List<Traduction> traduction);
        Task<bool> UpdateCompanyFinancialInformation(CompanyFinancialInformation obj, List<Traduction> traduction);
    }
}
