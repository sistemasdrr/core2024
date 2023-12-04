using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.DTO.Core.Response;
using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Transversal.Common;
using System;

namespace DRRCore.Application.Interfaces.CoreApplication
{
    public interface ISubscriberApplication
    {
        Task<Response<GetSubscriberRequestDto>> GetSubscriberById(int id);
        Task<Response<bool>> DeleteSubscriber(int id);
        Task<Response<bool>> ActiveSubscriber(int id);
        Task<Response<int>> AddOrUpdateAsync(AddOrUpdateSubscriberRequestDto subscriberDto);
        Task<Response<List<GetListSubscriberResponseDto>>> GetSubscriber(string code, string name, string enable);
    }
}
