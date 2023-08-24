using DRRCore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DRRCore.Services.ApiCore.Controllers
{
    [ApiController]
    [Route("[controller]")]  
    public class ApiController : Controller
    {
        private readonly IApiApplication _apiApplication;
        private readonly ITokenValidationApplication _tokenValidation;
        public ApiController(IApiApplication apiApplication, ITokenValidationApplication tokenValidationApplication)
        {
            _apiApplication = apiApplication;
            _tokenValidation = tokenValidationApplication;
        }
        [HttpGet()]
        [Route("get/dev/code/{code}")]
        public async Task<ActionResult> GetCodeByDevelopmentEnvironment()
        {
            var response = await _tokenValidation.ValidationTokenAndEnvironmentAsync("D");
            if (!response.IsSuccess)
            {
                return Unauthorized();
            }
            return Ok(await _tokenValidation.ValidationTokenAsync());
        }
        [HttpGet()]
        [Route("get/code/{code}")]
        public async Task<ActionResult> GetCodeByProductionEnvironment()
        {
            var response = await _tokenValidation.ValidationTokenAndEnvironmentAsync("D");
            if (!response.IsSuccess)
            {
                return Unauthorized();
            }
            return Ok(await _apiApplication.GetDummyReportAsync());
        }
        [HttpGet()]
        [Route("dummy")]
        public async Task<ActionResult> DummyReport()
        {
            return Ok(await _apiApplication.GetDummyReportAsync());
        }
    }
}
