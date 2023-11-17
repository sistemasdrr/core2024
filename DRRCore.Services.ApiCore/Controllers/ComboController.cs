using DRRCore.Application.Interfaces.CoreApplication;
using Microsoft.AspNetCore.Mvc;

namespace DRRCore.Services.ApiCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComboController : Controller
    {
        private readonly IComboboxApplication _comboboxApplication;
        public ComboController(IComboboxApplication comboboxApplication)
        {
            _comboboxApplication = comboboxApplication;
        }

        [HttpGet()]
        [Route("doctype")]
        public async Task<ActionResult> GetDocumentTypeCombo()
        {
            return Ok(await _comboboxApplication.GetDocumentTypeNatural());
        }
        [HttpGet()]
        [Route("civilstatus")]
        public async Task<ActionResult> GetCivilStatusCombo()
        {
            return Ok(await _comboboxApplication.GetCivilStatus());
        }
        [HttpGet()]
        [Route("continent")]
        public async Task<ActionResult> GetContinentsCombo()
        {
            return Ok(await _comboboxApplication.GetContinents());
        }
        [HttpGet()]
        [Route("country")]
        public async Task<ActionResult> GetCountriesCombo()
        {
            return Ok(await _comboboxApplication.GetCountries());
        }
        [HttpGet()]
        [Route("countrybycontinent")]
        public async Task<ActionResult> GetCountriesByContinentCombo(int continent)
        {
            return Ok(await _comboboxApplication.GetCountriesByContinent(continent));
        }
        [HttpGet()]
        [Route("jobdep")]
        public async Task<ActionResult> GetJobDepartmentCombo()
        {
            return Ok(await _comboboxApplication.GetDepartment());
        }
        [HttpGet()]
        [Route("jobbydep")]
        public async Task<ActionResult> GetJobByDepartmentCombo(int department)
        {
            return Ok(await _comboboxApplication.GetJobByDepartment(department));
        }
        [HttpGet()]
        [Route("currency")]
        public async Task<ActionResult> GetCurrencyCombo()
        {
            return Ok(await _comboboxApplication.GetCurrency());
        }
        [HttpGet()]
        [Route("bankaccounttype")]
        public async Task<ActionResult> GetBankAccountTypeCombo()
        {
            return Ok(await _comboboxApplication.GetBankAccountType());
        }
        [HttpGet()]
        [Route("fambondytype")]
        public async Task<ActionResult> GetFamilyBondyTypeCombo()
        {
            return Ok(await _comboboxApplication.GetFamilyBondyType());
        }
        [HttpGet()]
        [Route("legalpersontype")]
        public async Task<ActionResult> GetLegalPersonTypeCombo()
        {
            return Ok(await _comboboxApplication.GetLegalPersonType());
        }
        [HttpGet()]
        [Route("creditrisk")]
        public async Task<ActionResult> GetCreditRiskCombo()
        {
            return Ok(await _comboboxApplication.GetCreditRisk());
        }
        [HttpGet()]
        [Route("companyreputation")]
        public async Task<ActionResult> GetCompanyReputationCombo()
        {
            return Ok(await _comboboxApplication.GetCompanyReputation());
        }
        [HttpGet()]
        [Route("paymentpolicy")]
        public async Task<ActionResult> GetPaymentPolicyCombo()
        {
            return Ok(await _comboboxApplication.GetPaymentPolicy());
        }
        [HttpGet()]
        [Route("legalregister")]
        public async Task<ActionResult> GetLegalRegisterSituationCombo()
        {
            return Ok(await _comboboxApplication.GetLegalRegisterSituation());
        }
    }
}
