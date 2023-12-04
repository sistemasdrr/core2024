using DRRCore.Application.DTO.Core.Request;
using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Transversal.Common;

namespace DRRCore.Application.Interfaces.CoreApplication
{
    public interface ISubscriberApplication
    {
        Task<Response<Subscriber>> GetSubscriberById(int id);
        Task<Response<int>> AddOrUpdateAsync(AddOrUpdateSubscriberRequestDto subscriberDto);
        Task<Response<List<Subscriber>>> GetSubscriber(string code, string name, Boolean enable);
    }
}
