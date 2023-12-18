using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Infraestructure.Interfaces.CoreRepository
{
    public interface IComercialLatePaymentRepository : IBaseRepository<ComercialLatePayment>
    {
        Task<List<ComercialLatePayment>> GetComercialLatePaymetByIdCompany(int idCompany);
    }
}
