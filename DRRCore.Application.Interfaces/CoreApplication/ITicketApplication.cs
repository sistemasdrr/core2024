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
        Task<Response<GetTicketRequestDto>> GetTicketRequestAsync(int id);
        Task<Response<List<GetListTicketResponseDto>>> GetTicketListAsync();
        Task<Response<bool>> DeleteTicket(int id);
        Task<Response<List<GetListTicketResponseDto>>> GetTicketListPendingAsync();
        Task<Response<List<GetListTicketResponseDto>>> GetTicketListByAsync(string ticket, string name, string subscriber, string type, string procedure);
        Task<Response<GetTicketQueryResponseDto>> GetTicketQuery(int idTicket);
        Task<Response<bool>> AnswerTicket(int idTicket);
        Task<Response<bool>> SendTicketQuery(SendTicketQueryRequestDto request);
        Task<Response<byte[]>> DownloadReport();
        Task<Response<bool>> SavePreAsignTicket(List<SavePreAsignTicketDto> lista);
        Task<Response<bool>> SendPreAsignTicket(List<SavePreAsignTicketDto> lista);
        Task<Response<List<GetListTicketResponseDto>>> GetTicketsToUser(string userTo);
        Task<Response<List<GetPersonalAssignationResponseDto>>> GetPersonalAssignation();
        Task<Response<List<GetPersonalAssignationResponseDto>>> GetAgentAssignation();
    }
}
