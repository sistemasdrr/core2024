using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Domain.Interfaces.CoreDomain
{
    public interface ISubscriberPriceDomain : IBaseDomain<SubscriberPrice>
    {
        Task<List<SubscriberPrice>> GetPricesBySubscriberId(int idSubscriber);
    }
}
