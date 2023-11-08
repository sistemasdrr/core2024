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
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public ComboboxApplication(IDocumentTypeDomain documentTypeDomain,
                                   ICivilStatusDomain civilStatusDomain,
                                   ICountryDomain countryDomain,
                                   IJobDepartmentDomain jobDepartmentDomain,
                                   IFamilyBondyTypeDomain familyBondyTypeDomain,
                                   IBankAccountTypeDomain bankAccountTypeDomain,
                                   ICurrencyDomain currencyDomain,
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
    }
}
