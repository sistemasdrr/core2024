using DRRCore.Domain.Entities.SqlCoreContext;

namespace DRRCore.Domain.Interfaces.CoreDomain
{
    public interface ISubscriberDomain
    {
        Task<Subscriber> GetSubscriberById(int id);
        Task<int> AddSubscriberAsync(Subscriber subscriber);
        Task<Boolean> UpdateSubscriberAsync(Subscriber subscriber);
        Task<List<Subscriber>> GetSubscriber(string code, string name, Boolean enable);
    }
}
