using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Transversal.Common;

namespace DRRCore.Application.Interfaces.CoreApplication
{
    public interface IXmlApplication
    {
        Task<Response<CompanyXmlData>> GetXmlCompanyAsync(int idCompany);
    }
}
