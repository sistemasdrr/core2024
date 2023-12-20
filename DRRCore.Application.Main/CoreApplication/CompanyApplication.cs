using AutoMapper;
using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.DTO.Core.Response;
using DRRCore.Application.Interfaces.CoreApplication;
using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Domain.Interfaces.CoreDomain;
using DRRCore.Transversal.Common;
using DRRCore.Transversal.Common.Interface;

namespace DRRCore.Application.Main.CoreApplication
{
    public class CompanyApplication : ICompanyApplication
    {
        private readonly ICompanyDomain _companyDomain;
        private readonly ICompanyBackgroundDomain _companyBackgroundDomain;
        private readonly ICompanyFinancialInformationDomain _companyFinancialInformationDomain;
        private readonly IFinancialSalesHistoryDomain _financialSalesHistoryDomain;
        private readonly IFinancialBalanceDomain _financialBalanceDomain;
        private readonly IProviderDomain _providerDomain;
        private readonly IComercialLatePaymentDomain _comercialLatePaymentDomain;
        private readonly IBankDebtDomain _bankDebtDomain;
        private readonly ICompanySBSDomain _companySBSDomain;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CompanyApplication(ICompanyDomain companyDomain,ICompanyBackgroundDomain companyBackgroundDomain,
            ICompanyFinancialInformationDomain companyFinancialInformationDomain, IMapper mapper, ILogger logger, 
            IFinancialSalesHistoryDomain financialSalesHistoryDomain, IFinancialBalanceDomain financialBalanceDomain,
            IProviderDomain providerDomain, IComercialLatePaymentDomain comercialLatePaymentDomain, IBankDebtDomain bankDebtDomain,
            ICompanySBSDomain companySBSDomain)
        {
            _companyDomain = companyDomain;
            _companyBackgroundDomain = companyBackgroundDomain;
            _companyFinancialInformationDomain = companyFinancialInformationDomain;
            _financialSalesHistoryDomain = financialSalesHistoryDomain;
            _financialBalanceDomain = financialBalanceDomain;
            _providerDomain = providerDomain;
            _comercialLatePaymentDomain = comercialLatePaymentDomain;
            _bankDebtDomain = bankDebtDomain;
            _companySBSDomain = companySBSDomain;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Response<int>> AddOrUpdateAsync(AddOrUpdateCompanyRequestDto obj)
        {
            List<Traduction> traductions = new List<Traduction>();
            var response = new Response<int>();
            try
            {
                if (obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.WrongParameter;
                    _logger.LogError(response.Message);
                    return response;
                }
                if (obj.Id == 0)
                {
                    foreach (var item in obj.Traductions)
                    {
                        traductions.Add(new Traduction
                        {
                            Identifier = item.Key,
                            ShortValue = item.Key.Split('_')[0] == "S" ? item.Value : string.Empty,
                            LargeValue = item.Key.Split('_')[0] == "L" ? item.Value : string.Empty,
                            IdLanguage=1,
                            LastUpdaterUser=1
                        });
                    }
                    var newCompany = _mapper.Map<Company>(obj);
                    newCompany.Traductions = traductions;
                     response.Data = await _companyDomain.AddCompanyAsync(newCompany);
                }
                else
                {
                    var existingCompany = await _companyDomain.GetByIdAsync(obj.Id);
                    if (existingCompany == null)
                    {
                        response.IsSuccess = false;
                        response.Message = Messages.MessageNoDataFoundEmployee;
                        _logger.LogError(response.Message);
                        return response;
                    }
                    existingCompany = _mapper.Map(obj, existingCompany);

                    foreach (var item in obj.Traductions)
                    {
                        traductions.Add(new Traduction
                        {
                            Identifier = item.Key,
                            ShortValue = item.Key.Split('_')[0] == "S" ? item.Value : string.Empty,
                            LargeValue = item.Key.Split('_')[0] == "L" ? item.Value : string.Empty,
                            IdLanguage = 1,
                            LastUpdaterUser = 1
                        });
                    }
                    existingCompany.Traductions= traductions;
                    existingCompany.UpdateDate = DateTime.Now;
                    await _companyDomain.UpdateAsync(existingCompany);
                    response.Data = existingCompany.Id;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> AddOrUpdateCompanyBackGroundAsync(AddOrUpdateCompanyBackgroundRequestDto obj)
        {
            List<Traduction> traductions = new List<Traduction>();
            var response = new Response<bool>();
            try
            {
                if (obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.WrongParameter;
                    _logger.LogError(response.Message);
                    return response;
                }
                if (obj.Id == 0)
                {
                    foreach (var item in obj.Traductions)
                    {
                        traductions.Add(new Traduction
                        {
                            Identifier = item.Key,
                            ShortValue = item.Key.Split('_')[0] == "S" ? item.Value : string.Empty,
                            LargeValue = item.Key.Split('_')[0] == "L" ? item.Value : string.Empty,
                            IdLanguage = 1,
                            LastUpdaterUser = 1
                        });
                    }
                    var newCompany = _mapper.Map<CompanyBackground>(obj);                   
                    response.Data = await _companyBackgroundDomain.AddAsync(newCompany,traductions);
                }
                else
                {
                    var existingCompany = await _companyBackgroundDomain.GetByIdAsync(obj.IdCompany);
                    if (existingCompany == null)
                    {
                        response.IsSuccess = false;
                        response.Message = Messages.MessageNoDataCompany;
                        _logger.LogError(response.Message);
                        return response;
                    }
                    existingCompany = _mapper.Map(obj, existingCompany);

                    foreach (var item in obj.Traductions)
                    {
                        traductions.Add(new Traduction
                        {
                            Identifier = item.Key,
                            ShortValue = item.Key.Split('_')[0] == "S" ? item.Value : string.Empty,
                            LargeValue = item.Key.Split('_')[0] == "L" ? item.Value : string.Empty,
                            IdLanguage = 1,
                            LastUpdaterUser = 1
                        });
                    }
                    existingCompany.UpdateDate = DateTime.Now;
                    response.Data = await _companyBackgroundDomain.UpdateAsync(existingCompany,traductions);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> DeleteAsync(int id)
        {
            var response = new Response<bool>();
            try
            {
                if (id == 0)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.WrongParameter;
                    _logger.LogError(response.Message);
                }
                response.Data = await _companyDomain.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<List<GetListCompanyResponseDto>>> GetAllCompanys(string name, string form, int idCountry, bool haveReport)
        {
            var response = new Response<List<GetListCompanyResponseDto>>();
            try
            {
                var company = await _companyDomain.GetByNameAsync(name,form,idCountry,haveReport);
                if (company == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                }
                response.Data = _mapper.Map<List<GetListCompanyResponseDto>>(company);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<GetCompanyResponseDto>> GetCompanyById(int id)
        {
            var response = new Response<GetCompanyResponseDto>();
            try
            {
                var company = await _companyDomain.GetByIdAsync(id);
                if (company == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<GetCompanyResponseDto>(company);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }
        public async Task<Response<GetCompanyBackgroundResponseDto>> GetCompanyBackgroundById(int id)
        {
            var response = new Response<GetCompanyBackgroundResponseDto>();
            try
            {
                var company = await _companyBackgroundDomain.GetByIdAsync(id);
                if (company == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<GetCompanyBackgroundResponseDto>(company);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> ActiveWebVisionAsync(int id)
        {
            var response = new Response<bool>();
            try
            {
                if (id == 0)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.WrongParameter;
                    _logger.LogError(response.Message);
                }
                response.Data = await _companyDomain.ActiveWebVision(id);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> DesactiveWebVisionAsync(int id)
        {
            var response = new Response<bool>();
            try
            {
                if (id == 0)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.WrongParameter;
                    _logger.LogError(response.Message);
                }
                response.Data = await _companyDomain.DesactiveWebVision(id);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<int>> AddOrUpdateCompanyFinancialInformationAsync(AddOrUpdateCompanyFinancialInformationRequestDto obj)
        {
            List<Traduction> traductions = new List<Traduction>();
            var response = new Response<int>();
            try
            {
                if (obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.WrongParameter;
                    _logger.LogError(response.Message);
                    return response;
                }
                if (obj.Id == 0)
                {
                    foreach (var item in obj.Traductions)
                    {
                        traductions.Add(new Traduction
                        {
                            Identifier = item.Key,
                            ShortValue = item.Key.Split('_')[0] == "S" ? item.Value : string.Empty,
                            LargeValue = item.Key.Split('_')[0] == "L" ? item.Value : string.Empty,
                            IdLanguage = 1,
                            LastUpdaterUser = 1
                        });
                    }
                    var newCompany = _mapper.Map<CompanyFinancialInformation>(obj);
                    response.Data = await _companyFinancialInformationDomain.AddCompanyFinancialInformation(newCompany, traductions);
                }
                else
                {
                    var existingCompany = await _companyFinancialInformationDomain.GetByIdCompany((int)obj.IdCompany);
                    if (existingCompany == null)
                    {
                        response.IsSuccess = false;
                        response.Message = Messages.MessageNoDataCompany;
                        _logger.LogError(response.Message);
                        return response;
                    }
                    existingCompany = _mapper.Map(obj, existingCompany);

                    foreach (var item in obj.Traductions)
                    {
                        traductions.Add(new Traduction
                        {
                            Identifier = item.Key,
                            ShortValue = item.Key.Split('_')[0] == "S" ? item.Value : string.Empty,
                            LargeValue = item.Key.Split('_')[0] == "L" ? item.Value : string.Empty,
                            IdLanguage = 1,
                            LastUpdaterUser = 1
                        });
                    }
                    existingCompany.UpdateDate = DateTime.Now;
                    response.Data = await _companyFinancialInformationDomain.UpdateCompanyFinancialInformation(existingCompany, traductions);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<GetCompanyFinancialInformationResponseDto>> GetCompanyFinancialInformationById(int id)
        {
            var response = new Response<GetCompanyFinancialInformationResponseDto>();
            try
            {
                var company = await _companyFinancialInformationDomain.GetByIdAsync(id);
                if (company == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<GetCompanyFinancialInformationResponseDto>(company);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<GetCompanyFinancialInformationResponseDto>> GetCompanyFinancialInformationByIdCompany(int idCompany)
        {
            var response = new Response<GetCompanyFinancialInformationResponseDto>();
            try
            {
                var company = await _companyFinancialInformationDomain.GetByIdCompany(idCompany);
                if (company == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<GetCompanyFinancialInformationResponseDto>(company);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> AddOrUpdateSaleHistoryAsync(AddOrUpdateSaleHistoryRequestDto obj)
        {
            var response = new Response<bool>();
            try
            {
                if(obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }else if(obj.Id == 0)
                {
                    var newSaleHistory = _mapper.Map<SalesHistory>(obj);
                    response.Data = await _financialSalesHistoryDomain.AddAsync(newSaleHistory);
                }
                else if(obj.Id > 0)
                {
                    var existingSaleHistory = await _financialSalesHistoryDomain.GetByIdAsync(obj.Id);
                    existingSaleHistory = _mapper.Map(obj, existingSaleHistory);
                    response.Data = await _financialSalesHistoryDomain.UpdateAsync(existingSaleHistory);
                }
            }catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<List<GetListSalesHistoryResponseDto>>> GetListSalesHistoriesByIdCompany(int idCompany)
        {
            var response = new Response<List<GetListSalesHistoryResponseDto>>();
            try
            {
                var list = await _financialSalesHistoryDomain.GetByIdCompany(idCompany);
                if(list == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }               
                response.Data = _mapper.Map<List<GetListSalesHistoryResponseDto>>(list);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<GetSaleHistoryResponseDto>> GetSaleHistoryById(int id)
        {
            var response = new Response<GetSaleHistoryResponseDto>();
            try
            {
                var saleHistory = await _financialSalesHistoryDomain.GetByIdAsync(id);
                if(saleHistory == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<GetSaleHistoryResponseDto>(saleHistory);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> DeleteSaleHistory(int id)
        {
            var response = new Response<bool>();
            try
            {
                if(id == 0 || id == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = await _financialSalesHistoryDomain.DeleteAsync(id);
            }catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> AddOrUpdateFinancialBalanceAsync(AddOrUpdateFinancialBalanceRequestDto obj)
        {
            var response = new Response<bool>();
            try
            {
                if (obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                if(obj.Id == 0)
                {
                    var newBalance = _mapper.Map<FinancialBalance>(obj);
                    response.Data = await _financialBalanceDomain.AddAsync(newBalance);
                }
                else
                {
                    var existingBalance = await _financialBalanceDomain.GetByIdAsync(obj.Id);
                    existingBalance = _mapper.Map(obj, existingBalance);
                    response.Data = await _financialBalanceDomain.UpdateAsync(existingBalance);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<List<GetComboValueResponseDto>>> GetListFinancialBalanceAsync(int idCompany, string balanceType)
        {
            var response = new Response<List<GetComboValueResponseDto>>();
            try
            {
                var list = await _financialBalanceDomain.GetFinancialBalanceByIdCompany(idCompany, balanceType);
                if(list == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
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

        public async Task<Response<GetFinancialBalanceResponseDto>> GetFinancialBalanceById(int id)
        {
            var response = new Response<GetFinancialBalanceResponseDto>();
            try
            {
                var balance = await _financialBalanceDomain.GetByIdAsync(id);
                if (balance == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<GetFinancialBalanceResponseDto>(balance);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> DeleteFinancialBalance(int id)
        {
            var response = new Response<bool>();
            try
            {
                var balance = await _financialBalanceDomain.GetByIdAsync(id);
                if (balance == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                else
                {
                    response.Data = await _financialBalanceDomain.DeleteAsync(id);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> AddOrUpdateProviderAsync(AddOrUpdateProviderRequestDto obj)
        {
            var response = new Response<bool>();
            try
            {
                if(obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                if (obj.Id == 0)
                {
                    var newProvider = _mapper.Map<Provider>(obj);
                    response.Data = await _providerDomain.AddAsync(newProvider);
                }
                else
                {
                    var existingProvider = await _providerDomain.GetByIdAsync(obj.Id);
                    existingProvider = _mapper.Map(obj, existingProvider);
                    response.Data = await _providerDomain.UpdateAsync(existingProvider);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<List<GetListProviderResponseDto>>> GetListProvidersAsync(int idCompany)
        {
            var response = new Response<List<GetListProviderResponseDto>>();
            try
            {
                var list = await _providerDomain.GetProvidersByIdCompany(idCompany);
                if (list == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<List<GetListProviderResponseDto>>(list);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<GetProviderResponseDto>> GetProviderById(int id)
        {
            var response = new Response<GetProviderResponseDto>();
            try
            {
                var provider = await _providerDomain.GetByIdAsync(id);
                if (provider == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<GetProviderResponseDto>(provider);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> DeleteProvider(int id)
        {
            var response = new Response<bool>();
            try
            {
                var provider = await _providerDomain.GetByIdAsync(id);
                if (provider == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                else
                {
                    response.Data = await _providerDomain.DeleteAsync(id);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> AddOrUpdateComercialLatePaymentAsync(AddOrUpdateComercialLatePaymentRequestDto obj)
        {
            var response = new Response<bool>();
            try
            {
                if (obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                if (obj.Id == 0)
                {
                    var newComercialLatePayment = _mapper.Map<ComercialLatePayment>(obj);
                    response.Data = await _comercialLatePaymentDomain.AddAsync(newComercialLatePayment);
                }
                else
                {
                    var existingComercialLatePayment = await _comercialLatePaymentDomain.GetByIdAsync(obj.Id);
                    existingComercialLatePayment = _mapper.Map(obj, existingComercialLatePayment);
                    response.Data = await _comercialLatePaymentDomain.UpdateAsync(existingComercialLatePayment);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<List<GetListComercialLatePaymentResponseDto>>> GetListComercialLatePaymentAsync(int idCompany)
        {
            var response = new Response<List<GetListComercialLatePaymentResponseDto>>();
            try
            {
                var list = await _comercialLatePaymentDomain.GetComercialLatePaymetByIdCompany(idCompany);
                if (list == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<List<GetListComercialLatePaymentResponseDto>>(list);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<GetComercialLatePaymentResponseDto>> GetComercialLatePaymentById(int id)
        {
            var response = new Response<GetComercialLatePaymentResponseDto>();
            try
            {
                var comercialLatePayment = await _comercialLatePaymentDomain.GetByIdAsync(id);
                if (comercialLatePayment == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<GetComercialLatePaymentResponseDto>(comercialLatePayment);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> DeleteComercialLatePayment(int id)
        {
            var response = new Response<bool>();
            try
            {
                var comercialLatePayment = await _comercialLatePaymentDomain.GetByIdAsync(id);
                if (comercialLatePayment == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                else
                {
                    response.Data = await _comercialLatePaymentDomain.DeleteAsync(id);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> AddOrUpdateBankDebtAsync(AddOrUpdateBankDebtRequestDto obj)
        {
            var response = new Response<bool>();
            try
            {
                if (obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                if (obj.Id == 0)
                {
                    var newBankDebt = _mapper.Map<BankDebt>(obj);
                    response.Data = await _bankDebtDomain.AddAsync(newBankDebt);
                }
                else
                {
                    var existingBankDebt = await _bankDebtDomain.GetByIdAsync(obj.Id);
                    existingBankDebt = _mapper.Map(obj, existingBankDebt);
                    response.Data = await _bankDebtDomain.UpdateAsync(existingBankDebt);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<List<GetListBankDebtResponseDto>>> GetListBankDebtAsync(int idCompany)
        {
            var response = new Response<List<GetListBankDebtResponseDto>>();
            try
            {
                var list = await _bankDebtDomain.GetBankDebtsByIdCompany(idCompany);
                if (list == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<List<GetListBankDebtResponseDto>>(list);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<GetBankDebtResponseDto>> GetBankDebtById(int id)
        {
            var response = new Response<GetBankDebtResponseDto>();
            try
            {
                var bankDebt = await _bankDebtDomain.GetByIdAsync(id);
                if (bankDebt == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<GetBankDebtResponseDto>(bankDebt);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> DeleteBankDebt(int id)
        {
            var response = new Response<bool>();
            try
            {
                var bankDebt = await _bankDebtDomain.GetByIdAsync(id);
                if (bankDebt == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                else
                {
                    response.Data = await _bankDebtDomain.DeleteAsync(id);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> AddOrUpdateCompanySBSAsync(AddOrUpdateCompanySbsRequestDto obj)
        {
            var response = new Response<bool>();
            try
            {
                if (obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                if (obj.Id == 0)
                {
                    var newCompanySbs= _mapper.Map<CompanySb>(obj);
                    response.Data = await _companySBSDomain.AddAsync(newCompanySbs);
                }
                else
                {
                    var existingCompanySbs = await _companySBSDomain.GetByIdAsync(obj.Id);
                    existingCompanySbs = _mapper.Map(obj, existingCompanySbs);
                    response.Data = await _companySBSDomain.UpdateAsync(existingCompanySbs);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<GetCompanySbsResponseDto>> GetCompanySBSById(int id)
        {
            var response = new Response<GetCompanySbsResponseDto>();
            try
            {
                var companySbs = await _companySBSDomain.GetByIdAsync(id);
                if (companySbs == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<GetCompanySbsResponseDto>(companySbs);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> DeleteCompanySBS(int id)
        {
            var response = new Response<bool>();
            try
            {
                var bankDebt = await _companySBSDomain.GetByIdAsync(id);
                if (bankDebt == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                else
                {
                    response.Data = await _companySBSDomain.DeleteAsync(id);
                }
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
