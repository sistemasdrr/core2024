﻿using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.Interfaces.CoreApplication;
using DRRCore.Domain.Entities.SqlCoreContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Xml.Serialization;
using DRRCore.Application.DTO.Core.Response;
using DRRCore.Transversal.Common;

namespace DRRCore.Services.ApiCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : Controller
    {
        private readonly ICompanyApplication _companyApplication;
        private readonly ICompanyImagesApplication _companyImagesApplication;
        private readonly IXmlApplication _xmlApplication;
        public CompanyController(IXmlApplication xmlApplication, ICompanyApplication companyApplication, ICompanyImagesApplication companyImagesApplication)
        {
            _companyApplication = companyApplication;
            _companyImagesApplication = companyImagesApplication;
            _xmlApplication = xmlApplication;
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
        public async Task<ActionResult> GetCompanyByName(string? name,string? form,int idCountry,bool haveReport,bool similar)
        {
            name ??= string.Empty;
            form ??= string.Empty;
            return Ok(await _companyApplication.GetAllCompanys(name,form,idCountry,haveReport,similar));
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
        [HttpGet()]
        [Route("getCompanyBranch")]
        public async Task<ActionResult> getCompanyBranch(int idCompany)
        {
            return Ok(await _companyApplication.GetCompanyBranchByIdCompany(idCompany));
        }
        [HttpPost()]
        [Route("addCompanyBranch")]
        public async Task<ActionResult> addCompanyBranch(AddOrUpdateCompanyBranchRequestDto obj)
        {
            return Ok(await _companyApplication.AddOrUpdateCompanyBranchAsync(obj));
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
        [HttpPost()]
        [Route("addOrUpdateGeneralInformation")]
        public async Task<ActionResult> addOrUpdateGeneralInformation(AddOrUpdateCompanyGeneralInformationRequestDto obj)
        {
            return Ok(await _companyApplication.AddOrUpdateGeneralInformation(obj));
        }
        [HttpGet()]
        [Route("getGeneralInformationByIdCompany")]
        public async Task<ActionResult> getGeneralInformationByIdCompany(int idCompany)
        {
            return Ok(await _companyApplication.GetGeneralInformationByIdCompany(idCompany));
        }
        [HttpPost()]
        [Route("addOrUpdateCompanyImg")]
        public async Task<ActionResult> addOrUpdateCompanyImg(AddOrUpdateCompanyImagesRequestDto obj)
        {
            return Ok(await _companyImagesApplication.AddOrUpdateImages(obj));
        }
        [HttpGet()]
        [Route("getCompanyImgByIdCompany")]
        public async Task<ActionResult> getCompanyImgByIdCompany(int idCompany)
        {
            return Ok(await _companyImagesApplication.GetCompanyImagesByIdCompany(idCompany));
        }
        [HttpPost()]
        [Route("uploadImage")]
        public async Task<ActionResult> uploadImage( IFormFile request)
        {
            return Ok(await _companyImagesApplication.UploadImage(request));
        }
        [HttpGet()]
        [Route("getImageByPath")]
        public async Task<ActionResult> getImageByPath(string path)
        {
            var result = await _companyImagesApplication.GetImageByPath(path);

            if (result != null && result.Data != null)
            {
                return File(result.Data.File?.ToArray(), result.Data.ContentType, result.Data.FileName);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet()]
        [Route("getBase64ByPath")]
        public async Task<IActionResult> GetBase64ByPath(string path)
        {
            try
            {
                var result = await _companyImagesApplication.GetBase64eByPath(path);

                if (result.Data == null)
                {
                    var errorResponse = new Response<string>
                    {
                        Message = result.Message
                    };

                    var serializer = new XmlSerializer(typeof(Response<string>));
                    var stringWriter = new System.IO.StringWriter();
                    serializer.Serialize(stringWriter, errorResponse);

                    Response.ContentType = "application/xml";
                    return Content(stringWriter.ToString(), "application/xml");
                }
                else
                {
                    var response = new Response<string>
                    {
                        Data = result.Data
                    };

                    var serializer = new XmlSerializer(typeof(Response<string>));
                    var stringWriter = new System.IO.StringWriter();
                    serializer.Serialize(stringWriter, response);

                    Response.ContentType = "application/xml";
                    return Content(stringWriter.ToString(), "application/xml");
                }
            }
            catch (Exception ex)
            {
                // Capturar y manejar la excepción
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }


        [HttpPost()]
        [Route("deleteImportAndExport")]
        public async Task<ActionResult> deleteImportAndExport(int id)
        {
            return Ok(await _companyApplication.DeleteImportAndExport(id));
        }
        [HttpPost()]
        [Route("addImportAndExport")]
        public async Task<ActionResult> addImportAndExport(AddOrUpdateImportsAndExportsRequestDto obj)
        {
            return Ok(await _companyApplication.AddOrUpdateImportAndExport(obj));
        }
        [HttpGet()]
        [Route("getImportAndExportById")]
        public async Task<ActionResult> getImportAndExportById(int id)
        {
            return Ok(await _companyApplication.GetImportAndExportById(id));
        }
        [HttpGet()]
        [Route("getImportsAndExportsByIdCompany")]
        public async Task<ActionResult> getImportAndExportById(int idCompany, string type)
        {
            return Ok(await _companyApplication.GetListImportAndExportByIdCompany(idCompany, type));
        }
        [HttpPost()]
        [Route("addCompanyPartner")]
        public async Task<ActionResult> addCompanyPartner(AddOrUpdateCompanyPartnersRequestDto obj)
        {
            return Ok(await _companyApplication.AddOrUpdateCompanyPartner(obj));
        }
        [HttpGet()]
        [Route("getCompanyPartner")]
        public async Task<ActionResult> getCompanyPartner(int id)
        {
            return Ok(await _companyApplication.GetCompanyPartnerById(id));
        }
        [HttpPost()]
        [Route("deleteCompanyPartner")]
        public async Task<ActionResult> deleteCompanyPartner(int id)
        {
            return Ok(await _companyApplication.DeleteCompanyPartner(id));
        }
        [HttpGet()]
        [Route("getListCompanyPartner")]
        public async Task<ActionResult> getListCompanyPartner(int idCompany)
        {
            return Ok(await _companyApplication.GetListCompanyPartnerByIdCompany(idCompany));
        }
        [HttpPost()]
        [Route("addCompanyShareHolder")]
        public async Task<ActionResult> addCompanyShareHolder(AddOrUpdateCompanyShareHolderRequestDto obj)
        {
            return Ok(await _companyApplication.AddOrUpdateCompanyShareHolder(obj));
        }
        [HttpGet()]
        [Route("getCompanyShareHolder")]
        public async Task<ActionResult> getCompanyShareHolder(int id)
        {
            return Ok(await _companyApplication.GetCompanyShareHolderById(id));
        }
        [HttpPost()]
        [Route("deleteCompanyShareHolder")]
        public async Task<ActionResult> deleteCompanyShareHolder(int id)
        {
            return Ok(await _companyApplication.DeleteCompanyShareHolder(id));
        }
        [HttpGet()]
        [Route("getListCompanyShareHolder")]
        public async Task<ActionResult> getListCompanyShareHolder(int idCompany)
        {
            return Ok(await _companyApplication.GetListCompanyShareHolderByIdCompany(idCompany));
        }
        [HttpPost()]
        [Route("addWorkerHistory")]
        public async Task<ActionResult> addWorkerHistory(AddOrUpdateWorkerHistoryRequestDto obj)
        {
            return Ok(await _companyApplication.AddOrUpdateWorkerHistory(obj));
        }
        [HttpGet()]
        [Route("getWorkerHistory")]
        public async Task<ActionResult> getWorkerHistory(int id)
        {
            return Ok(await _companyApplication.GetWorkerHistoryById(id));
        }
        [HttpPost()]
        [Route("deleteWorkerHistory")]
        public async Task<ActionResult> deleteWorkerHistory(int id)
        {
            return Ok(await _companyApplication.DeleteWorkerHistory(id));
        }
        [HttpGet()]
        [Route("getListWorkerHistory")]
        public async Task<ActionResult> getListWorkerHistory(int idCompany)
        {
            return Ok(await _companyApplication.GetListWorkerHistoryByIdCompany(idCompany));
        }
        [HttpPost()]
        [Route("addCompanyRelation")]
        public async Task<ActionResult> addCompanyRelation(AddOrUpdateCompanyRelationRequestDto obj)
        {
            return Ok(await _companyApplication.AddOrUpdateCompanyRelation(obj));
        }
        [HttpGet()]
        [Route("getCompanyRelation")]
        public async Task<ActionResult> getCompanyRelation(int id)
        {
            return Ok(await _companyApplication.GetCompanyRelationById(id));
        }
        [HttpPost()]
        [Route("deleteCompanyRelation")]
        public async Task<ActionResult> deleteCompanyRelation(int id)
        {
            return Ok(await _companyApplication.DeleteCompanyRelation(id));
        }
        [HttpGet()]
        [Route("getListCompanyRelation")]
        public async Task<ActionResult> getListCompanyRelation(int idCompany)
        {
            return Ok(await _companyApplication.GetListCompanyRelationByIdCompany(idCompany));
        }
        [HttpGet()]
        [Route("getStatus")]
        public async Task<ActionResult> getStatus(int idCompany)
        {
            return Ok(await _companyApplication.GetStatusCompany(idCompany));
        }
        [HttpGet()]
        [Route("getf1")]
        public async Task<IActionResult> GetF1(int idCompany,string language, string format)
        {
            var result = await _companyApplication.DownloadF1(idCompany,language,format);
            
            return File(result.Data.File, result.Data.ContentType, result.Data.Name);
        }
        [HttpGet()]
        [Route("getf8")]
        public async Task<IActionResult> GetF8(int idCompany, string language, string format)
        {
            var result = await _companyApplication.DownloadF8(idCompany, language, format);

            return File(result.Data.File, result.Data.ContentType, result.Data.Name);
        }
        [HttpGet()]
        [Route("getXmlCredendo")]
        public async Task<IActionResult> getXmlCompany(int idTicket)
        {
            var result = await _xmlApplication.GetXmlCredendoAsync(idTicket);

            return File(result.Data.File, result.Data.ContentType, result.Data.Name);
        }
        [HttpGet()]
        [Route("getXmlGeneral")]
        public async Task<IActionResult> getXmlGeneral(int idTicket)
        {
            var result = await _xmlApplication.GetXmlAtradiusAsync(idTicket);

            return File(result.Data.File, result.Data.ContentType, result.Data.Name);
        }
    }
}
