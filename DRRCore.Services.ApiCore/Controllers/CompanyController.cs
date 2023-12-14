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
        [HttpPost()]
        [Route("get")]
        public async Task<ActionResult> GetCompany(int id)
        {
            return Ok(await _companyApplication.GetCompanyById(id));
        }
        [HttpPost()]
        [Route("getbyname")]
        public async Task<ActionResult> GetCompanyByName(string? name,string? form,int idCountry,bool haveReport)
        {
            name ??= string.Empty;
            form ??= string.Empty;
            return Ok(await _companyApplication.GetAllCompanys(name,form,idCountry,haveReport));
        }
        [HttpGet()]
        [Route("getBack")]
        public async Task<ActionResult> GetCompanyBackground(int idCompany)
        {
             return Ok(await _companyApplication.GetCompanyBackgroundById(idCompany));
        }
        [HttpPost()]
        [Route("addBack")]
        public async Task<ActionResult> AddBackground(AddOrUpdateCompanyBackgroundRequestDto obj)
        {
           return Ok(await _companyApplication.AddOrUpdateCompanyBackGroundAsync(obj));
        }
        [HttpPost()]
        [Route("activeweb")]
        public async Task<ActionResult> ActiveWebVision(int id)
        {
            return Ok(await _companyApplication.ActiveWebVisionAsync(id));
        }
        [HttpPost()]
        [Route("desactiveweb")]
        public async Task<ActionResult> DesactiveWebVision(int id)
        {
            return Ok(await _companyApplication.DesactiveWebVisionAsync(id));
        }
        [HttpPost()]
        [Route("addFinancial")]
        public async Task<ActionResult> AddFinancialInformation(AddOrUpdateCompanyFinancialInformationRequestDto obj)
        {
            return Ok(await _companyApplication.AddOrUpdateCompanyFinancialInformationAsync(obj));
        }
        [HttpGet()]
        [Route("getFinancialById")]
        public async Task<ActionResult> GetFinancialById(int id)
        {
            return Ok(await _companyApplication.GetCompanyFinancialInformationById(id));
        }
        [HttpGet()]
        [Route("getFinancialByIdCompany")]
        public async Task<ActionResult> GetFinancialByIdCompany(int idCompany)
        {
            return Ok(await _companyApplication.GetCompanyFinancialInformationByIdCompany(idCompany));
        }
        [HttpPost()]
        [Route("addOrUpdateSaleHistory")]
        public async Task<ActionResult> addOrUpdateSaleHistory(AddOrUpdateSaleHistoryRequestDto obj)
        {
            return Ok(await _companyApplication.AddOrUpdateSaleHistoryAsync(obj));
        }
        [HttpGet()]
        [Route("getSaleHistoryById")]
        public async Task<ActionResult> getSaleHistoryById(int id)
        {
            return Ok(await _companyApplication.GetSaleHistoryById(id));
        }
        [HttpGet()]
        [Route("getListSaleHistoryByIdCompany")]
        public async Task<ActionResult> getListSaleHistoryByIdCompany(int idCompany)
        {
            return Ok(await _companyApplication.GetListSalesHistoriesByIdCompany(idCompany));
        }
    }
}
