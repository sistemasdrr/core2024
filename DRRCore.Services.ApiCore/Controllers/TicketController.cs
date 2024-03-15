﻿using DRRCore.Application.DTO.Core.Request;
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
        public async Task<ActionResult> answeredTicketQuery(int idTicket)
        {
            return Ok(await _ticketApplication.AnswerTicket(idTicket));
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
    }
}
