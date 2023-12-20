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
        [HttpPost()]
        [Route("deleteSaleHistory")]
        public async Task<ActionResult> deleteSaleHistory(int id)
        {
            return Ok(await _companyApplication.DeleteSaleHistory(id));
        }
        [HttpPost()]
        [Route("addOrUpdateBalance")]
        public async Task<ActionResult> addOrUpdateBalance(AddOrUpdateFinancialBalanceRequestDto obj)
        {
            return Ok(await _companyApplication.AddOrUpdateFinancialBalanceAsync(obj));
        }
        [HttpGet()]
        [Route("getListBalance")]
        public async Task<ActionResult> getListBalance(int idCompany, string balanceType)
        {
            return Ok(await _companyApplication.GetListFinancialBalanceAsync(idCompany, balanceType));
        }
        [HttpGet()]
        [Route("getBalanceById")]
        public async Task<ActionResult> getBalanceById(int id)
        {
            return Ok(await _companyApplication.GetFinancialBalanceById(id));
        }
        [HttpPost()]
        [Route("deleteBalance")]
        public async Task<ActionResult> deleteBalance(int id)
        {
            return Ok(await _companyApplication.DeleteFinancialBalance(id));
        }
        [HttpPost()]
        [Route("addOrUpdateCompanySbs")]
        public async Task<ActionResult> addOrUpdateCompanySbs(AddOrUpdateCompanySbsRequestDto obj)
        {
            return Ok(await _companyApplication.AddOrUpdateCompanySBSAsync(obj));
        }
        [HttpGet()]
        [Route("getCompanySbsByIdCompany")]
        public async Task<ActionResult> getCompanySbsByIdCompany(int idCompany)
        {
            return Ok(await _companyApplication.GetCompanySBSById(idCompany));
        }
        [HttpPost()]
        [Route("deleteCompanySbs")]
        public async Task<ActionResult> deleteCompanySbs(int id)
        {
            return Ok(await _companyApplication.DeleteCompanySBS(id));
        }
        [HttpPost()]
        [Route("addOrUpdateProvider")]
        public async Task<ActionResult> addOrUpdateProvider(AddOrUpdateProviderRequestDto obj)
        {
            return Ok(await _companyApplication.AddOrUpdateProviderAsync(obj));
        }
        [HttpGet()]
        [Route("getListProvider")]
        public async Task<ActionResult> getListProvider(int idCompany)
        {
            return Ok(await _companyApplication.GetListProvidersAsync(idCompany));
        }
        [HttpGet()]
        [Route("getProviderById")]
        public async Task<ActionResult> getProviderById(int id)
        {
            return Ok(await _companyApplication.GetProviderById(id));
        }
        [HttpPost()]
        [Route("deleteProvider")]
        public async Task<ActionResult> deleteProvider(int id)
        {
            return Ok(await _companyApplication.DeleteProvider(id));
        }
        [HttpPost()]
        [Route("addOrUpdateLatePayment")]
        public async Task<ActionResult> addOrUpdateLatePayment(AddOrUpdateComercialLatePaymentRequestDto obj)
        {
            return Ok(await _companyApplication.AddOrUpdateComercialLatePaymentAsync(obj));
        }
        [HttpGet()]
        [Route("getListLatePayment")]
        public async Task<ActionResult> getListLatePayment(int idCompany)
        {
            return Ok(await _companyApplication.GetListComercialLatePaymentAsync(idCompany));
        }
        [HttpGet()]
        [Route("getLatePaymentById")]
        public async Task<ActionResult> getLatePaymentById(int id)
        {
            return Ok(await _companyApplication.GetComercialLatePaymentById(id));
        }
        [HttpPost()]
        [Route("deleteLatePayment")]
        public async Task<ActionResult> deleteLatePayment(int id)
        {
            return Ok(await _companyApplication.DeleteComercialLatePayment(id));
        }
        [HttpPost()]
        [Route("addOrUpdateBankDebt")]
        public async Task<ActionResult> addOrUpdateBankDebt(AddOrUpdateBankDebtRequestDto obj)
        {
            return Ok(await _companyApplication.AddOrUpdateBankDebtAsync(obj));
        }
        [HttpGet()]
        [Route("getListBankDebt")]
        public async Task<ActionResult> getListBankDebt(int idCompany)
        {
            return Ok(await _companyApplication.GetListBankDebtAsync(idCompany));
        }
        [HttpGet()]
        [Route("getBankDebtById")]
        public async Task<ActionResult> getBankDebtById(int id)
        {
            return Ok(await _companyApplication.GetBankDebtById(id));
        }
        [HttpPost()]
        [Route("deleteBankDebt")]
        public async Task<ActionResult> deleteBankDebt(int id)
        {
            return Ok(await _companyApplication.DeleteBankDebt(id));
        }
        [HttpPost()]
        [Route("addOrUpdateEndorsement")]
        public async Task<ActionResult> addOrUpdateEndorsement(AddOrUpdateEndorsementsRequestDto obj)
        {
            return Ok(await _companyApplication.AddOrUpdateEndorsementsAsync(obj));
        }
        [HttpGet()]
        [Route("getListEndorsement")]
        public async Task<ActionResult> getListEndorsement(int idCompany)
        {
            return Ok(await _companyApplication.GetListEndorsementsAsync(idCompany));
        }
        [HttpGet()]
        [Route("getEndorsementById")]
        public async Task<ActionResult> getEndorsementById(int id)
        {
            return Ok(await _companyApplication.GetEndorsementsById(id));
        }
        [HttpPost()]
        [Route("deleteEndorsement")]
        public async Task<ActionResult> deleteEndorsement(int id)
        {
            return Ok(await _companyApplication.DeleteEndorsements(id));
        }
        [HttpPost()]
        [Route("addOrUpdateCreditOpinion")]
        public async Task<ActionResult> addOrUpdateCreditOpinion(AddOrUpdateCompanyCreditOpinionRequestDto obj)
        {
            return Ok(await _companyApplication.AddOrUpdateCreditOpinionAsync(obj));
        }
        [HttpGet()]
        [Route("getCreditOpinionByIdCompany")]
        public async Task<ActionResult> getCreditOpinionByIdCompany(int idCompany)
        {
            return Ok(await _companyApplication.GetCreditOpinionByIdCompany(idCompany));
        }
        [HttpPost()]
        [Route("deleteCreditOpinion")]
        public async Task<ActionResult> deleteCreditOpinion(int id)
        {
            return Ok(await _companyApplication.DeleteCreditOpinion(id));
        }
    }
}
