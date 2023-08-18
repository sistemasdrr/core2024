using DRRCore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DRRCore.Services.ApiCore.Controllers
{
    [ApiController]
    [Route("[controller]")]  
    public class ApiController : Controller
    {
        private readonly IApiApplication _apiApplication;
        public ApiController(IApiApplication apiApplication)
        {
            _apiApplication = apiApplication;
        }
       
        [HttpGet()]
        [Route("dummy")]
        public async Task<ActionResult> DummyReport()
        {
            return Ok(await _apiApplication.GetDummyReportAsync());
        }
    }
}
