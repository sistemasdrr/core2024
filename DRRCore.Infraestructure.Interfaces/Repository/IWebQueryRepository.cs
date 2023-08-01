using DRRCore.Domain.Entities.MYSQLContext;
using DRRCore.Domain.Entities.SQLContext;

namespace DRRCore.Infraestructure.Interfaces.Repository
{
    public interface IWebQueryRepository
    {
        Task<bool> UpdateData(List<ViewConsultaWeb> listWebData);
        Task<List<WebQuery>> GetByParamAsync(string param, int page);
        Task<WebQuery> GetByCodeAsync(string code);
    }
}
