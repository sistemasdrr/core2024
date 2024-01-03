using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.Interfaces.CoreApplication;
using DRRCore.Application.Main.CoreApplication;
using Microsoft.AspNetCore.Mvc;

namespace DRRCore.Services.ApiCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly IPersonApplication _personApplication;
        public PersonController(IPersonApplication personApplication)
        {
            _personApplication = personApplication;
        }

        [HttpPost()]
        [Route("addPerson")]
        public async Task<ActionResult> addPerson(AddOrUpdatePersonRequestDto request)
        {
            return Ok(await _personApplication.AddOrUpdatePerson(request));
        }
        [HttpPost()]
        [Route("deletePerson")]
        public async Task<ActionResult> deletePerson(int id)
        {
            return Ok(await _personApplication.DeletePerson(id));
        }
        [HttpPost()]
        [Route("activateWeb")]
        public async Task<ActionResult> activateWeb(int id)
        {
            return Ok(await _personApplication.ActivateWebPerson(id));
        }
        [HttpPost()]
        [Route("desactivateWeb")]
        public async Task<ActionResult> desactivateWeb(int id)
        {
            return Ok(await _personApplication.DesactivateWebPerson(id));
        }
        [HttpGet()]
        [Route("getListPerson")]
        public async Task<ActionResult> getListPerson(string? fullname, string? form, int idCountry, bool haveReport)
        {
            fullname ??= string.Empty;
            form ??= string.Empty;
            return Ok(await _personApplication.GetListPerson(fullname, form, idCountry, haveReport));
        }
        [HttpGet()]
        [Route("getPerson")]
        public async Task<ActionResult> getPerson(int id)
        {
            return Ok(await _personApplication.GetPersonById(id));
        }
    }
}
