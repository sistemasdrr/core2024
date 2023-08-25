using CoreFtp.Infrastructure;
using DRRCore.Application.Interfaces;
using DRRCore.Transversal.Common;
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
        public async Task<ActionResult> GetCodeByDevelopmentEnvironment(string code)
        {
            var response = await _tokenValidation.ValidationTokenAndEnvironmentAsync(Transversal.Common.Constants.DevelopmenteEnvironment, code);
            if (!response.IsSuccess)
            {
                return Unauthorized();
            }
            return Ok(await _apiApplication.GetDummyReportAsync());
        }
        [HttpGet()]
        [Route("get/qa/code/{code}")]
        public async Task<ActionResult> GetCodeByQualityEnvironment(string code)
        {
            var response = await _tokenValidation.ValidationTokenAndEnvironmentAsync(Transversal.Common.Constants.QualityEnvironment, code);
            if (!response.IsSuccess)
            {
                return Unauthorized();
            }
            return Ok(response);
        }
        [HttpGet()]
        [Route("get/code/{code}")]
        public async Task<ActionResult> GetCodeByProductionEnvironment(string code)
        {
            var response = await _tokenValidation.ValidationTokenAndEnvironmentAsync(Transversal.Common.Constants.ProductionEnvironment, code);
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
