using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.Interfaces.CoreApplication;
using Microsoft.AspNetCore.Mvc;

namespace DRRCore.Services.ApiCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : Controller
    {
        private readonly ICompanyApplication _companyApplication;
        public CompanyController(ICompanyApplication companyApplication)
        {
            _companyApplication = companyApplication;
        }
        [HttpPost()]
        [Route("add")]
        public async Task<ActionResult> AddCompany(AddOrUpdateCompanyRequestDto request)
        {
            return Ok(await _companyApplication.AddOrUpdateAsync(request));
        }
        [HttpPost()]
        [Route("delete")]
        public async Task<ActionResult> DeleteCompany(int id)
        {
            return Ok(await _companyApplication.DeleteAsync(id));
        }
    }
}
