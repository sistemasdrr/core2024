using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.DTO.Core.Response;
using DRRCore.Transversal.Common;

namespace DRRCore.Application.Interfaces.CoreApplication
{
    public interface ISubscriberPriceApplication
    {
        Task<Response<List<GetSuscriberPriceResponseDto>>> GetSubscriberPricesById(int idSubscriber);
        Task<Response<GetSuscriberPriceByIdResponseDto>> GetSubscriberPriceById(int id);
        Task<Response<bool>> AddOrUpdateAsync (AddOrUpdateSubscriberPriceDto obj);
        Task<Response<bool>> DeleteAsync (int id);
    }
}
