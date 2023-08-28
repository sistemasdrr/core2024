using DRRCore.Application.DTO.API;
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
        [HttpPost()]
        [Route("search")]
        public async Task<ActionResult> SearchByParamAndCountry(SearchRequestDto request)
        {
            var response = await _tokenValidation.ValidationTokenAndEnvironmentAsync(Transversal.Common.Constants.ProductionEnvironment);
            if (!response.IsSuccess)
            {
                return Unauthorized();
            }
            return Ok(await _apiApplication.Search(request, Transversal.Common.Constants.ProductionEnvironment));
        }
        [HttpPost()]
        [Route("qa/search")]
        public async Task<ActionResult> SearchByParamAndCountryQuality(SearchRequestDto request)
        {
            var response = await _tokenValidation.ValidationTokenAndEnvironmentAsync(Transversal.Common.Constants.QualityEnvironment);
            if (!response.IsSuccess)
            {
                return Unauthorized();
            }
            return Ok(await _apiApplication.Search(request, Transversal.Common.Constants.QualityEnvironment));
        }
        [HttpPost()]
        [Route("dev/search")]
        public async Task<ActionResult> SearchByParamAndCountryDevelop(SearchRequestDto request)
        {
            //var response = await _tokenValidation.ValidationTokenAndEnvironmentAsync(Transversal.Common.Constants.DevelopmenteEnvironment);
            //if (!response.IsSuccess)
            //{
            //    return Unauthorized();
            //}
            return Ok(await _apiApplication.Search(request, Transversal.Common.Constants.DevelopmenteEnvironment));
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
        //[HttpGet()]
       
        //[Route("dummy")]
        //public async Task<ActionResult> DummyReport()
        //{
        //    return Ok(await _apiApplication.GetDummyReportAsync());
        //}
    }
}
