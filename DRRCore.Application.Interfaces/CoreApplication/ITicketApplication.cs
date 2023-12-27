using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.DTO.Core.Response;
using DRRCore.Transversal.Common;

namespace DRRCore.Application.Interfaces.CoreApplication
{
    public interface ITicketApplication
    {      
        Task<Response<bool>> AddTicketAsync(AddOrUpdateTicketRequestDto request);
        Task<Response<GetExistingTicketResponseDto>> GetReportType(int id, string type);
        Task<Response<GetNumerationResponseDto>> GetTicketNumberAsync();
    }
}
