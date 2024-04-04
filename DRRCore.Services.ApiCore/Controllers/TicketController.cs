using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.Interfaces.CoreApplication;
using DRRCore.Application.Main.CoreApplication;
using Microsoft.AspNetCore.Mvc;

namespace DRRCore.Services.ApiCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : Controller
    {
        public readonly ITicketApplication _ticketApplication;
        public TicketController(ITicketApplication ticketApplication)
        {
            _ticketApplication = ticketApplication;
        }

        [HttpGet()]
        [Route("numberticket")]
        public async Task<ActionResult> GetNumberTicket()
        {
            return Ok(await _ticketApplication.GetTicketNumberAsync());
        }
        [HttpGet()]
        [Route("getNumTicketById")]
        public async Task<ActionResult> getNumTicketById(int idTicket)
        {
            return Ok(await _ticketApplication.GetNumCuponById(idTicket));
        }
        [HttpGet()]
        [Route("getTicketsBySubscriber")]
        public async Task<ActionResult> getTicketsBySubscriber(int idSubscriber, string? company, DateTime from, DateTime until, int idCountry)
        {
            return Ok(await _ticketApplication.GetTicketsByIdSubscriber(idSubscriber, company, from, until, idCountry));
        }
        
        [HttpPost()]
        [Route("add")]
        public async Task<ActionResult> AddTicket(AddOrUpdateTicketRequestDto request)
        {
            return Ok(await _ticketApplication.AddTicketAsync(request));
        }
        [HttpPost()]
        [Route("addByWeb")]
        public async Task<ActionResult> AddTicketByWeb(AddOrUpdateTicketRequestDto request)
        {
            return Ok(await _ticketApplication.AddTicketByWeb(request));
        }
        [HttpPost()]
        [Route("addOnline")]
        public async Task<ActionResult> AddTicketOnline(AddOrUpdateTicketRequestDto request, string rubro, string sendTo)
        {
            rubro ??= string.Empty;
            sendTo ??= string.Empty;
            return Ok(await _ticketApplication.AddTicketOnline(request,rubro, sendTo));
        }
        [HttpGet()]
        [Route("getTicketHistorySubscriber")]
        public async Task<ActionResult> getTicketHistorySubscriber(int idSubscriber, string? name, DateTime? from, DateTime? until, int? idCountry)
        {
            name ??= string.Empty;
            from ??= null;
            until ??= null;
            return Ok(await _ticketApplication.GetTicketHistoryByIdSubscriber(idSubscriber, name, from, until, idCountry));
        }
        [HttpGet()]
        [Route("getreporttype")]
        public async Task<ActionResult> GetReportType(int id, string type)
        {
            return Ok(await _ticketApplication.GetReportType(id, type));
        }
        [HttpGet()]
        [Route("getTicketById")]
        public async Task<ActionResult> getTicketById(int id)
        {
            return Ok(await _ticketApplication.GetTicketRequestAsync(id));
        }
        [HttpGet()]
        [Route("getList")]
        public async Task<ActionResult> getList()
        {
            return Ok(await _ticketApplication.GetTicketListAsync());
        }
        [HttpGet()]
        [Route("getListby")]
        public async Task<ActionResult> getListBy(string? ticket, string? name, string? subscriber, string? type, string? procedure)
        {
            return Ok(await _ticketApplication.GetTicketListByAsync(ticket ?? string.Empty, name ?? string.Empty, subscriber ?? string.Empty, type ?? string.Empty, procedure ?? string.Empty));
        }
        [HttpPost()]
        [Route("deleteTicket")]
        public async Task<ActionResult> deleteTicket(int id)
        {
            return Ok(await _ticketApplication.DeleteTicket(id));
        }
        [HttpGet()]
        [Route("getListPending")]
        public async Task<ActionResult> getListPending()
        {
            return Ok(await _ticketApplication.GetTicketListPendingAsync());

        }
        [HttpGet()]
        [Route("getTicketQuery")]
        public async Task<ActionResult> getTicketQuery(int idTicket)
        {
            return Ok(await _ticketApplication.GetTicketQuery(idTicket));

        }
        [HttpPost()]
        [Route("answeredTicketQuery")]
        public async Task<ActionResult> answeredTicketQuery(int idTicket, string response)
        {
            return Ok(await _ticketApplication.AnswerTicket(idTicket, response));
        }
        [HttpPost()]
        [Route("sendQuery")]
        public async Task<ActionResult> sendQuery(SendTicketQueryRequestDto request)
        {
            return Ok(await _ticketApplication.SendTicketQuery(request));
        }
        [HttpGet()]
        [Route("report")]
        public async Task<IActionResult> Report()
        {
            var result = await _ticketApplication.DownloadReport();
            return File(result.Data, "application/vnd.ms-excel", "ReporteTickets" + DateTime.Now.ToString("ddMMyyyy") + ".xls");
        }

        [HttpPost()]
        [Route("saveTicketPreassignations")]
        public async Task<ActionResult> saveTicketAssignations(List<SavePreAsignTicketDto> list)
        {
            return Ok(await _ticketApplication.SavePreAsignTicket(list));
        }
        [HttpPost()]
        [Route("sendTicketPreassignations")]
        public async Task<ActionResult> sendTicketPreassignations(List<SavePreAsignTicketDto> list)
        {
            return Ok(await _ticketApplication.SendPreAsignTicket(list));
        }
        [HttpGet()]
        [Route("getTicketPreassignToUser")]
        public async Task<ActionResult> getTicketPreassignToUser(string userTo)
        {
            return Ok(await _ticketApplication.GetTicketsToUser(userTo));
        }
        [HttpGet()]
        [Route("getPersonalAssignation")]
        public async Task<ActionResult> GetPersonalAssignation()
        {
            return Ok(await _ticketApplication.GetPersonalAssignation());
        }
        [HttpGet()]
        [Route("getAgentAssignation")]
        public async Task<ActionResult> getAgentAssignation()
        {
            return Ok(await _ticketApplication.GetAgentAssignation());
        }
        [HttpPost()]
        [Route("uploadFile")]
        public async Task<ActionResult> uploadFile(int idTicket, string numCupon, IFormFile file)
        {
            return Ok(await _ticketApplication.UploadFile(idTicket, numCupon, file));
        }
        [HttpGet()]
        [Route("getFilesByIdTicket")]
        public async Task<ActionResult> getFilesByIdTicket(int idTicket)
        {
            return Ok(await _ticketApplication.GetTicketFilesByIdTicket(idTicket));
        }
        [HttpGet()]
        [Route("getFileByPath")]
        public async Task<ActionResult> getFileByPath(string path)
        {
            var result = await _ticketApplication.DownloadFileByPath(path);

            if (result != null && result.Data != null)
            {
                return File(result.Data.File?.ToArray(), result.Data.ContentType, result.Data.FileName);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost()]
        [Route("deleteFile")]
        public async Task<ActionResult> deleteFile(int id)
        {
            return Ok(await _ticketApplication.DeleteFile(id));
        }
        [HttpPost()]
        [Route("assignTicket")]
        public async Task<ActionResult> AssignTicket(List<AssignTicketRequestDto> list)
        {
            return Ok(await _ticketApplication.AssignTicket(list));
        }
    }
}
