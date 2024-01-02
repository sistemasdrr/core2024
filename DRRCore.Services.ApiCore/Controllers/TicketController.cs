using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.Interfaces.CoreApplication;
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

        [HttpPost()]
        [Route("add")]
        public async Task<ActionResult> AddTicket(AddOrUpdateTicketRequestDto request)
        {
            return Ok(await _ticketApplication.AddTicketAsync(request));
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
            return Ok(await _ticketApplication.GetTicketListByAsync(ticket??string.Empty,name ?? string.Empty, subscriber ?? string.Empty, type ?? string.Empty, procedure ?? string.Empty));
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
    }
}
