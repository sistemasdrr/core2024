using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Infraestructure.Interfaces.CoreRepository
{
    public interface ICompanyPartnersRepository : IBaseRepository<CompanyPartner>
    {
        Task<List<CompanyPartner>> GetPartnersByIdCompany(int idCompany);
    }
}
