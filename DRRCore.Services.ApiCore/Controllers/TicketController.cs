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
    }
}
