using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Infraestructure.Interfaces.CoreRepository
{
    public interface ISubscriberPriceRepository : IBaseRepository<SubscriberPrice>
    {
        Task<List<SubscriberPrice>> GetPricesBySubscriberId(int idSubscriber);
    }
}
