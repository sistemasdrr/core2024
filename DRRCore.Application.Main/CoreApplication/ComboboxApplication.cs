using AutoMapper;
using DRRCore.Application.DTO.Core.Response;
using DRRCore.Application.Interfaces.CoreApplication;
using DRRCore.Domain.Interfaces.CoreDomain;
using DRRCore.Transversal.Common;
using DRRCore.Transversal.Common.Interface;

namespace DRRCore.Application.Main.CoreApplication
{
    public class ComboboxApplication : IComboboxApplication
    {
        private readonly IDocumentTypeDomain _documentTypeDomain;
        private readonly ICivilStatusDomain _civilStatusDomain;
        private readonly ICountryDomain _countryDomain;
        private readonly IJobDepartmentDomain _jobDepartmentDomain;
        private readonly IJobDomain _jobDomain;
        private readonly ICurrencyDomain _currencyDomain;
        private readonly IFamilyBondyTypeDomain _familyBondyTypeDomain;
        private readonly IBankAccountTypeDomain _bankAccountTypeDomain;
        private readonly ILegalPersonTypeDomain _legalPersonTypeDomain;
        private readonly ICreditRiskDomain _creditRiskDomain;
        private readonly IPaymentPolicyDomain _paymentPolicyDomain;
        private readonly IReputationDomain _reputationDomain;
        private readonly ILegalRegisterSituationDomain _legalRegisterSituationDomain;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public ComboboxApplication(IDocumentTypeDomain documentTypeDomain,
                                   ICivilStatusDomain civilStatusDomain,
                                   ICountryDomain countryDomain,
                                   IJobDepartmentDomain jobDepartmentDomain,
                                   IFamilyBondyTypeDomain familyBondyTypeDomain,
                                   IBankAccountTypeDomain bankAccountTypeDomain,
                                   ICurrencyDomain currencyDomain,
                                   ILegalPersonTypeDomain legalPersonTypeDomain,
                                   ICreditRiskDomain creditRiskDomain,
                                   IPaymentPolicyDomain paymentPolicyDomain,
                                   IReputationDomain reputationDomain,
                                   ILegalRegisterSituationDomain legalRegisterSituationDomain,
                                   IJobDomain jobDomain,
                                    IMapper mapper,ILogger logger) { 
        
            _documentTypeDomain = documentTypeDomain;
            _mapper = mapper;
            _logger = logger;
            _civilStatusDomain = civilStatusDomain;
            _countryDomain = countryDomain;
            _jobDepartmentDomain = jobDepartmentDomain;
            _currencyDomain = currencyDomain;
            _bankAccountTypeDomain = bankAccountTypeDomain;
            _familyBondyTypeDomain = familyBondyTypeDomain;
            _legalPersonTypeDomain = legalPersonTypeDomain;
            _creditRiskDomain = creditRiskDomain;
            _paymentPolicyDomain = paymentPolicyDomain;
            _legalRegisterSituationDomain = legalRegisterSituationDomain;
            _reputationDomain= reputationDomain;
            _jobDomain = jobDomain;
        }

        public async Task<Response<List<GetComboValueResponseDto>>> GetBankAccountType()
        {
            var response = new Response<List<GetComboValueResponseDto>>();
            try
            {
                var list = await _bankAccountTypeDomain.GetAllAsync();
                response.Data = _mapper.Map<List<GetComboValueResponseDto>>(list);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<List<GetComboValueResponseDto>>> GetCivilStatus()
        {
            var response = new Response<List<GetComboValueResponseDto>>();
            try
            {
                var list = await _civilStatusDomain.GetAllAsync();
                response.Data = _mapper.Map<List<GetComboValueResponseDto>>(list);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<List<GetComboColorResponseDto>>> GetCompanyReputation()
        {
            var response = new Response<List<GetComboColorResponseDto>>();
            try
            {
                var list = await _reputationDomain.GetAllCompanyReputationAsync();
                response.Data = _mapper.Map<List<GetComboColorResponseDto>>(list);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<List<GetComboValueResponseDto>>> GetContinents()
        {
            var response = new Response<List<GetComboValueResponseDto>>();
            try
            {
                var list = await _countryDomain.GetContinents();
                response.Data = _mapper.Map<List<GetComboValueResponseDto>>(list);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<List<GetComboValueFlagResponseDto>>> GetCountries()
        {
            var response = new Response<List<GetComboValueFlagResponseDto>>();
            try
            {
                var list = await _countryDomain.GetAllAsync();
                response.Data = _mapper.Map<List<GetComboValueFlagResponseDto>>(list);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<List<GetComboValueFlagResponseDto>>> GetCountriesByContinent(int idContinent)
        {
            var response = new Response<List<GetComboValueFlagResponseDto>>();
            try
            {
                var list = await _countryDomain.GetCountriesByContinent(idContinent);
                response.Data = _mapper.Map<List<GetComboValueFlagResponseDto>>(list);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<List<GetComboCreditRiskResponseDto>>> GetCreditRisk()
        {
            var response = new Response<List<GetComboCreditRiskResponseDto>>();
            try
            {
                var list = await _creditRiskDomain.GetAllAsync();
                response.Data = _mapper.Map<List<GetComboCreditRiskResponseDto>>(list);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<List<GetComboValueResponseDto>>> GetCurrency()
        {
            var response = new Response<List<GetComboValueResponseDto>>();
            try
            {
                var list = await _currencyDomain.GetAllAsync();
                response.Data = _mapper.Map<List<GetComboValueResponseDto>>(list);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<List<GetComboValueResponseDto>>> GetDepartment()
        {
            var response = new Response<List<GetComboValueResponseDto>>();
            try
            {
                var list = await _jobDepartmentDomain.GetAllAsync();
                response.Data = _mapper.Map<List<GetComboValueResponseDto>>(list);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<List<GetComboValueResponseDto>>> GetDocumentTypeNatural()
        {
            var response = new Response<List<GetComboValueResponseDto>>();
            try
            {
                var list = await _documentTypeDomain.GetAllNaturalDocumentAsync();               
                response.Data = _mapper.Map<List<GetComboValueResponseDto>>(list);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<List<GetComboValueResponseDto>>> GetFamilyBondyType()
        {
            var response = new Response<List<GetComboValueResponseDto>>();
            try
            {
                var list = await _familyBondyTypeDomain.GetAllAsync();
                response.Data = _mapper.Map<List<GetComboValueResponseDto>>(list);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<List<GetComboValueResponseDto>>> GetJobByDepartment(int idDepartment)
        {
            var response = new Response<List<GetComboValueResponseDto>>();
            try
            {
                var list = await _jobDomain.GetJobByDepartment(idDepartment);
                response.Data = _mapper.Map<List<GetComboValueResponseDto>>(list);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<List<GetComboValueResponseDto>>> GetLegalPersonType()
        {
            var response = new Response<List<GetComboValueResponseDto>>();
            try
            {
                var list = await _legalPersonTypeDomain.GetAllAsync();
                response.Data = _mapper.Map<List<GetComboValueResponseDto>>(list);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<List<GetComboValueResponseDto>>> GetLegalRegisterSituation()
        {
            var response = new Response<List<GetComboValueResponseDto>>();
            try
            {
                var list = await _legalRegisterSituationDomain.GetAllAsync();
                response.Data = _mapper.Map<List<GetComboValueResponseDto>>(list);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<List<GetComboColorResponseDto>>> GetPaymentPolicy()
        {
            var response = new Response<List<GetComboColorResponseDto>>();
            try
            {
                var list = await _paymentPolicyDomain.GetAllAsync();
                response.Data = _mapper.Map<List<GetComboColorResponseDto>>(list);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }
    }
}
