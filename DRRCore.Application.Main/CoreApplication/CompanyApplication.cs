﻿using AspNetCore.Reporting;
using AutoMapper;
using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.DTO.Core.Response;
using DRRCore.Application.Interfaces.CoreApplication;
using DRRCore.Domain.Entities.MYSQLContext;
using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Domain.Interfaces.CoreDomain;
using DRRCore.Domain.Interfaces.MysqlDomain;
using DRRCore.Transversal.Common;
using DRRCore.Transversal.Common.Interface;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DRRCore.Application.Main.CoreApplication
{
    public class CompanyApplication : ICompanyApplication
    {
        private readonly int idUser;
        private readonly ClaimsIdentity claims;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITCuponDomain _tCuponDomain;
        private readonly ICompanyDomain _companyDomain;
        private readonly ICompanyBackgroundDomain _companyBackgroundDomain;
        private readonly ICompanyBranchDomain _companyBranchDomain;
        private readonly ICompanyFinancialInformationDomain _companyFinancialInformationDomain;
        private readonly IFinancialSalesHistoryDomain _financialSalesHistoryDomain;
        private readonly IFinancialBalanceDomain _financialBalanceDomain;
        private readonly IProviderDomain _providerDomain;
        private readonly IComercialLatePaymentDomain _comercialLatePaymentDomain;
        private readonly IBankDebtDomain _bankDebtDomain;
        private readonly ICompanySBSDomain _companySBSDomain;
        private readonly IEndorsementsDomain _endorsementsDomain;
        private readonly ICompanyCreditOpinionDomain _companyCreditOpinionDomain;
        private readonly ICompanyGeneralInformationDomain _companyGeneralInformationDomain;
        private readonly IImportsAndExportsDomain _importsAndExportsDomain;
        private readonly ICompanyPartnersDomain _companyPartnersDomain;
        private readonly ICompanyShareHolderDomain _companyShareHolderDomain;
        private readonly IWorkerHistoryDomain _workerHistoryDomain;
        private readonly ICompanyRelationDomain _companyRelationDomain;
        private readonly IReportingDownload _reportingDownload;
        private readonly ICompanyImagesDomain _companyImagesDomain;
        private readonly ITicketDomain _ticketDomain;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CompanyApplication(ICompanyDomain companyDomain,ICompanyBackgroundDomain companyBackgroundDomain, ICompanyBranchDomain companyBranchDomain,
            ICompanyFinancialInformationDomain companyFinancialInformationDomain, IMapper mapper, ILogger logger, ICompanyShareHolderDomain companyShareHolderDomain,
            IFinancialSalesHistoryDomain financialSalesHistoryDomain, IFinancialBalanceDomain financialBalanceDomain, IWorkerHistoryDomain workerHistoryDomain,
            IProviderDomain providerDomain, IComercialLatePaymentDomain comercialLatePaymentDomain, IBankDebtDomain bankDebtDomain, ICompanyRelationDomain companyRelationDomain,
            ICompanySBSDomain companySBSDomain, IEndorsementsDomain endorsementsDomain, ICompanyCreditOpinionDomain companyCreditOpinionDomain, ICompanyImagesDomain companyImagesDomain,
            ICompanyGeneralInformationDomain companyGeneralInformationDomain,
            IReportingDownload reportingDownload,
            ITicketDomain ticketDomain,
            ITCuponDomain tCuponDomain,
            IImportsAndExportsDomain importsAndExportsDomain, ICompanyPartnersDomain companyPartnersDomain, IHttpContextAccessor httpContextAccessor)
        {
            _companyDomain = companyDomain;
            _companyBackgroundDomain = companyBackgroundDomain;
            _companyBranchDomain = companyBranchDomain;
            _companyFinancialInformationDomain = companyFinancialInformationDomain;
            _financialSalesHistoryDomain = financialSalesHistoryDomain;
            _financialBalanceDomain = financialBalanceDomain;
            _providerDomain = providerDomain;
            _comercialLatePaymentDomain = comercialLatePaymentDomain;
            _bankDebtDomain = bankDebtDomain;
            _companySBSDomain = companySBSDomain;
            _endorsementsDomain = endorsementsDomain;
            _companyCreditOpinionDomain = companyCreditOpinionDomain;
            _companyGeneralInformationDomain = companyGeneralInformationDomain;
            _importsAndExportsDomain = importsAndExportsDomain;
            _companyPartnersDomain = companyPartnersDomain;
            _companyShareHolderDomain = companyShareHolderDomain;
            _workerHistoryDomain = workerHistoryDomain;
            _companyRelationDomain = companyRelationDomain;
            _ticketDomain= ticketDomain;           
            _companyImagesDomain = companyImagesDomain;
            _mapper = mapper;
            _logger = logger;
            _reportingDownload=reportingDownload;
            _httpContextAccessor = httpContextAccessor;
            claims = (ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity;
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

        public async Task<Response<int?>> AddOrUpdateCompanyBackGroundAsync(AddOrUpdateCompanyBackgroundRequestDto obj)
        {
            List<Traduction> traductions = new List<Traduction>();
            var response = new Response<int?>();
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

        public async Task<Response<List<GetListCompanyResponseDto>>> GetAllCompanys(string name, string form, int idCountry, bool haveReport, bool similar)
        {
            var response = new Response<List<GetListCompanyResponseDto>>();
            try
            {
                if (!similar)
                {
                    var company = await _companyDomain.GetByNameAsync(name, form, idCountry, haveReport, similar);
                    if (company == null)
                    {
                        response.IsSuccess = false;
                        response.Message = Messages.MessageNoDataFound;
                        _logger.LogError(response.Message);
                    }
                    response.Data = _mapper.Map<List<GetListCompanyResponseDto>>(company);
                }
                else
                {
                    var ticket = await _ticketDomain.GetByNameAsync(name,"E");
                    var mapper= _mapper.Map<List<GetListCompanyResponseDto>>(ticket);

                    var oldTicket = await _ticketDomain.GetSimilarByNameAsync(name,"E");
                    if (oldTicket.Any())
                    {
                        foreach (var item in oldTicket)
                        {
                            var company = await _companyDomain.GetByOldCode(item.Empresa);
                            if (company != null)
                            {
                                mapper.Add(new GetListCompanyResponseDto
                                {
                                    Name = item.NombreSolicitado,
                                    DispatchedName = item.NombreDespachado,
                                    Language = item.Idioma,
                                    Id = company.Id,
                                    Country =company.IdCountryNavigation.Name,
                                    IsoCountry= company.IdCountryNavigation.Iso,
                                    FlagCountry=company.IdCountryNavigation.FlagIso,
                                    Code=company.OldCode
                                });
                            }
                        }
                    }    
                    mapper =mapper.DistinctBy(x=>x.Name).DistinctBy(x => x.Code).ToList();
                    response.Data = mapper;
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

        public async Task<Response<int>> AddOrUpdateCompanySBSAsync(AddOrUpdateCompanySbsRequestDto obj)
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
                    var newCompanySbs = _mapper.Map<CompanySb>(obj);
                    response.Data = await _companySBSDomain.AddCompanySBS(newCompanySbs, traductions);
                }
                else
                {
                    var existingCompanySbs = await _companySBSDomain.GetByIdCompany((int)obj.IdCompany);
                    if (existingCompanySbs == null)
                    {
                        response.IsSuccess = false;
                        response.Message = Messages.MessageNoDataCompany;
                        _logger.LogError(response.Message);
                        return response;
                    }
                    existingCompanySbs = _mapper.Map(obj, existingCompanySbs);

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
                    existingCompanySbs.UpdateDate = DateTime.Now;
                    response.Data = await _companySBSDomain.UpdateCompanySBS(existingCompanySbs, traductions);
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

        public async Task<Response<bool>> AddOrUpdateEndorsementsAsync(AddOrUpdateEndorsementsRequestDto obj)
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
                    var newEndorsement = _mapper.Map<Endorsement>(obj);
                    response.Data = await _endorsementsDomain.AddAsync(newEndorsement);
                }
                else
                {
                    var existingEndorsement = await _endorsementsDomain.GetByIdAsync(obj.Id);
                    existingEndorsement = _mapper.Map(obj, existingEndorsement);
                    response.Data = await _endorsementsDomain.UpdateAsync(existingEndorsement);
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

        public async Task<Response<List<GetEndorsementsResponseDto>>> GetListEndorsementsAsync(int idCompany)
        {
            var response = new Response<List<GetEndorsementsResponseDto>>();
            try
            {
                var list = await _endorsementsDomain.GetByIdCompany(idCompany);
                if (list == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<List<GetEndorsementsResponseDto>>(list);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<GetEndorsementsResponseDto>> GetEndorsementsById(int id)
        {
            var response = new Response<GetEndorsementsResponseDto>();
            try
            {
                var endorsement = await _endorsementsDomain.GetByIdAsync(id);
                if (endorsement == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<GetEndorsementsResponseDto>(endorsement);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> DeleteEndorsements(int id)
        {
            var response = new Response<bool>();
            try
            {
                var endorsement = await _endorsementsDomain.GetByIdAsync(id);
                if (endorsement == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                else
                {
                    response.Data = await _endorsementsDomain.DeleteAsync(id);
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

        public async Task<Response<int>> AddOrUpdateCreditOpinionAsync(AddOrUpdateCompanyCreditOpinionRequestDto obj)
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
                    var newCreditOpinion = _mapper.Map<CompanyCreditOpinion>(obj);
                    response.Data = await _companyCreditOpinionDomain.AddCreditOpinion(newCreditOpinion, traductions);
                }
                else
                {
                    var existingCreditOpinion = await _companyCreditOpinionDomain.GetByIdCompany((int)obj.IdCompany);
                    if (existingCreditOpinion == null)
                    {
                        response.IsSuccess = false;
                        response.Message = Messages.MessageNoDataCompany;
                        _logger.LogError(response.Message);
                        return response;
                    }
                    existingCreditOpinion = _mapper.Map(obj, existingCreditOpinion);

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
                    existingCreditOpinion.UpdateDate = DateTime.Now;
                    response.Data = await _companyCreditOpinionDomain.UpdateCreditOpinion(existingCreditOpinion, traductions);
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

        public async Task<Response<GetCompanyCreditOpinionResponseDto>> GetCreditOpinionByIdCompany(int idCompany)
        {
            var response = new Response<GetCompanyCreditOpinionResponseDto>();
            try
            {
                var creditOpinion = await _companyCreditOpinionDomain.GetByIdCompany(idCompany);
                if (creditOpinion == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<GetCompanyCreditOpinionResponseDto>(creditOpinion);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> DeleteCreditOpinion(int id)
        {
            var response = new Response<bool>();
            try
            {
                var companyCreditOpinion = await _companyCreditOpinionDomain.GetByIdAsync(id);
                if (companyCreditOpinion == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                else
                {
                    response.Data = await _companyCreditOpinionDomain.DeleteAsync(id);
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

        public async Task<Response<int>> AddOrUpdateGeneralInformation(AddOrUpdateCompanyGeneralInformationRequestDto obj)
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
                    var newGeneralInformation = _mapper.Map<CompanyGeneralInformation>(obj);
                    response.Data = await _companyGeneralInformationDomain.AddGeneralInformation(newGeneralInformation, traductions);
                }
                else
                {
                    var existingGeneralInformation = await _companyGeneralInformationDomain.GetByIdCompany((int)obj.IdCompany);
                    if (existingGeneralInformation == null)
                    {
                        response.IsSuccess = false;
                        response.Message = Messages.MessageNoDataCompany;
                        _logger.LogError(response.Message);
                        return response;
                    }
                    existingGeneralInformation = _mapper.Map(obj, existingGeneralInformation);

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
                    existingGeneralInformation.UpdateDate = DateTime.Now;
                    response.Data = await _companyGeneralInformationDomain.UpdateGeneralInformation(existingGeneralInformation, traductions);
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

        public async Task<Response<GetCompanyGeneralInformationResponseDto>> GetGeneralInformationByIdCompany(int idCompany)
        {
            var response = new Response<GetCompanyGeneralInformationResponseDto>();
            try
            {
                var generalInformation = await _companyGeneralInformationDomain.GetByIdCompany(idCompany);
                if (generalInformation == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<GetCompanyGeneralInformationResponseDto>(generalInformation);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<int>> AddOrUpdateCompanyBranchAsync(AddOrUpdateCompanyBranchRequestDto obj)
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
                    var newCompanyBranch= _mapper.Map<CompanyBranch>(obj);
                    response.Data = await _companyBranchDomain.AddAsync(newCompanyBranch, traductions);
                }
                else
                {
                    var existingCompanyBranch= await _companyBranchDomain.GetCompanyBranchByIdCompany((int)obj.IdCompany);
                    if (existingCompanyBranch == null)
                    {
                        response.IsSuccess = false;
                        response.Message = Messages.MessageNoDataCompany;
                        _logger.LogError(response.Message);
                        return response;
                    }
                    existingCompanyBranch = _mapper.Map(obj, existingCompanyBranch);

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
                    existingCompanyBranch.UpdateDate = DateTime.Now;
                    response.Data = await _companyBranchDomain.UpdateAsync(existingCompanyBranch, traductions);
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

        public async Task<Response<GetCompanyBranchResponseDto>> GetCompanyBranchByIdCompany(int idCompany)
        {
            var response = new Response<GetCompanyBranchResponseDto>();
            try
            {
                var companyBranch = await _companyBranchDomain.GetCompanyBranchByIdCompany(idCompany);
                if (companyBranch == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<GetCompanyBranchResponseDto>(companyBranch);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> DeleteImportAndExport(int id)
        {
            var response = new Response<bool>();
            try
            {
                var importAndExport = await _importsAndExportsDomain.GetByIdAsync(id);
                if (importAndExport == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                else
                {
                    response.Data = await _importsAndExportsDomain.DeleteAsync(id);
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

        public async Task<Response<bool>> AddOrUpdateImportAndExport(AddOrUpdateImportsAndExportsRequestDto obj)
        {
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
                    var newImportAndExport = _mapper.Map<ImportsAndExport>(obj);
                    response.Data = await _importsAndExportsDomain.AddAsync(newImportAndExport);
                }
                else
                {
                    var existingImportAndExport = await _importsAndExportsDomain.GetByIdAsync((int)obj.Id);
                    if (existingImportAndExport == null)
                    {
                        response.IsSuccess = false;
                        response.Message = Messages.MessageNoDataCompany;
                        _logger.LogError(response.Message);
                        return response;
                    }
                    existingImportAndExport = _mapper.Map(obj, existingImportAndExport);
                    existingImportAndExport.UpdateDate = DateTime.Now;
                    response.Data = await _importsAndExportsDomain.UpdateAsync(existingImportAndExport);
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

        public async Task<Response<GetImportsAndExportResponseDto>> GetImportAndExportById(int id)
        {
            var response = new Response<GetImportsAndExportResponseDto>();
            try
            {
                var importAndExport = await _importsAndExportsDomain.GetByIdAsync(id);
                if (importAndExport == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<GetImportsAndExportResponseDto>(importAndExport);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<List<GetImportsAndExportResponseDto>>> GetListImportAndExportByIdCompany(int idCompany, string type)
        {
            var response = new Response<List<GetImportsAndExportResponseDto>>();
            try
            {
                if(type == "I")
                {
                    var list = await _importsAndExportsDomain.GetImports(idCompany);
                    response.Data = _mapper.Map<List<GetImportsAndExportResponseDto>>(list);

                }
                else if(type == "E") 
                {
                    var list = await _importsAndExportsDomain.GetExports(idCompany);
                    response.Data = _mapper.Map<List<GetImportsAndExportResponseDto>>(list);
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

        public async Task<Response<bool>> AddOrUpdateCompanyPartner(AddOrUpdateCompanyPartnersRequestDto obj)
        {
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
                    var newObj= _mapper.Map<CompanyPartner>(obj);
                    response.Data = await _companyPartnersDomain.AddAsync(newObj);
                }
                else
                {
                    var existingObj= await _companyPartnersDomain.GetByIdAsync((int)obj.Id);
                    if (existingObj == null)
                    {
                        response.IsSuccess = false;
                        response.Message = Messages.MessageNoDataCompany;
                        _logger.LogError(response.Message);
                        return response;
                    }
                    existingObj = _mapper.Map(obj, existingObj);
                    existingObj.UpdateDate = DateTime.Now;
                    response.Data = await _companyPartnersDomain.UpdateAsync(existingObj);
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

        public async Task<Response<GetCompanyPartnersResponseDto>> GetCompanyPartnerById(int id)
        {
            var response = new Response<GetCompanyPartnersResponseDto>();
            try
            {
                var obj = await _companyPartnersDomain.GetByIdAsync(id);
                if (obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<GetCompanyPartnersResponseDto>(obj);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> DeleteCompanyPartner(int id)
        {
            var response = new Response<bool>();
            try
            {
                var obj = await _companyPartnersDomain.GetByIdAsync(id);
                if (obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                else
                {
                    response.Data = await _companyPartnersDomain.DeleteAsync(id);
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

        public async Task<Response<List<GetListCompanyPartnersResponseDto>>> GetListCompanyPartnerByIdCompany(int idCompany)
        {
            var response = new Response<List<GetListCompanyPartnersResponseDto>>();
            try
            {
                var list = await _companyPartnersDomain.GetPartnersByIdCompany(idCompany);
                if(list != null)
                {
                    response.Data = _mapper.Map<List<GetListCompanyPartnersResponseDto>>(list);
                }
                else
                {
                    response.Data = null;
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

        public async Task<Response<bool>> AddOrUpdateCompanyShareHolder(AddOrUpdateCompanyShareHolderRequestDto obj)
        {
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
                    var newObj = _mapper.Map<CompanyShareHolder>(obj);
                    response.Data = await _companyShareHolderDomain.AddAsync(newObj);
                }
                else
                {
                    var existingObj = await _companyShareHolderDomain.GetByIdAsync((int)obj.Id);
                    if (existingObj == null)
                    {
                        response.IsSuccess = false;
                        response.Message = Messages.MessageNoDataCompany;
                        _logger.LogError(response.Message);
                        return response;
                    }
                    existingObj = _mapper.Map(obj, existingObj);
                    existingObj.UpdateDate = DateTime.Now;
                    response.Data = await _companyShareHolderDomain.UpdateAsync(existingObj);
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

        public async Task<Response<GetCompanyShareHolderResponseDto>> GetCompanyShareHolderById(int id)
        {
            var response = new Response<GetCompanyShareHolderResponseDto>();
            try
            {
                var obj = await _companyShareHolderDomain.GetByIdAsync(id);
                if (obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<GetCompanyShareHolderResponseDto>(obj);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> DeleteCompanyShareHolder(int id)
        {
            var response = new Response<bool>();
            try
            {
                var obj = await _companyShareHolderDomain.GetByIdAsync(id);
                if (obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                else
                {
                    response.Data = await _companyShareHolderDomain.DeleteAsync(id);
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

        public async Task<Response<List<GetListCompanyShareHolderResponseDto>>> GetListCompanyShareHolderByIdCompany(int idCompany)
        {
            var response = new Response<List<GetListCompanyShareHolderResponseDto>>();
            try
            {
                var list = await _companyShareHolderDomain.GetShareHoldersByIdCompany(idCompany);
                if (list != null)
                {
                    response.Data = _mapper.Map<List<GetListCompanyShareHolderResponseDto>>(list);
                }
                else
                {
                    response.Data = null;
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

        public async Task<Response<bool>> AddOrUpdateWorkerHistory(AddOrUpdateWorkerHistoryRequestDto obj)
        {
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
                    var newObj = _mapper.Map<WorkersHistory>(obj);
                    response.Data = await _workerHistoryDomain.AddAsync(newObj);
                }
                else
                {
                    var existingObj = await _workerHistoryDomain.GetByIdAsync((int)obj.Id);
                    if (existingObj == null)
                    {
                        response.IsSuccess = false;
                        response.Message = Messages.MessageNoDataCompany;
                        _logger.LogError(response.Message);
                        return response;
                    }
                    existingObj = _mapper.Map(obj, existingObj);
                    existingObj.UpdateDate = DateTime.Now;
                    response.Data = await _workerHistoryDomain.UpdateAsync(existingObj);
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

        public async Task<Response<GetWorkersHistoryResponseDto>> GetWorkerHistoryById(int id)
        {
            var response = new Response<GetWorkersHistoryResponseDto>();
            try
            {
                var obj = await _workerHistoryDomain.GetByIdAsync(id);
                if (obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<GetWorkersHistoryResponseDto>(obj);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> DeleteWorkerHistory(int id)
        {
            var response = new Response<bool>();
            try
            {
                var obj = await _workerHistoryDomain.GetByIdAsync(id);
                if (obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                else
                {
                    response.Data = await _workerHistoryDomain.DeleteAsync(id);
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

        public async Task<Response<List<GetListWorkersHistoryResponseDto>>> GetListWorkerHistoryByIdCompany(int idCompany)
        {
            var response = new Response<List<GetListWorkersHistoryResponseDto>>();
            try
            {
                var list = await _workerHistoryDomain.GetByIdCompanyAsync(idCompany);
                if (list != null)
                {
                    response.Data = _mapper.Map<List<GetListWorkersHistoryResponseDto>>(list);
                }
                else
                {
                    response.Data = null;
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

        public async Task<Response<bool>> AddOrUpdateCompanyRelation(AddOrUpdateCompanyRelationRequestDto obj)
        {
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
                    var newObj = _mapper.Map<CompanyRelation>(obj);
                    response.Data = await _companyRelationDomain.AddAsync(newObj);
                }
                else
                {
                    var existingObj = await _companyRelationDomain.GetByIdAsync((int)obj.Id);
                    if (existingObj == null)
                    {
                        response.IsSuccess = false;
                        response.Message = Messages.MessageNoDataCompany;
                        _logger.LogError(response.Message);
                        return response;
                    }
                    existingObj = _mapper.Map(obj, existingObj);
                    existingObj.UpdateDate = DateTime.Now;
                    response.Data = await _companyRelationDomain.UpdateAsync(existingObj);
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

        public async Task<Response<GetCompanyRelationResponseDto>> GetCompanyRelationById(int id)
        {
            var response = new Response<GetCompanyRelationResponseDto>();
            try
            {
                var obj = await _companyRelationDomain.GetByIdAsync(id);
                if (obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                response.Data = _mapper.Map<GetCompanyRelationResponseDto>(obj);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> DeleteCompanyRelation(int id)
        {
            var response = new Response<bool>();
            try
            {
                var obj = await _companyRelationDomain.GetByIdAsync(id);
                if (obj == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }
                else
                {
                    response.Data = await _companyRelationDomain.DeleteAsync(id);
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

        public async Task<Response<List<GetListCompanyRelationResponseDto>>> GetListCompanyRelationByIdCompany(int idCompany)
        {
            var response = new Response<List<GetListCompanyRelationResponseDto>>();
            try
            {
                var list = await _companyRelationDomain.GetCompanyRelationByIdCompany(idCompany);
                if (list != null)
                {
                    response.Data = _mapper.Map<List<GetListCompanyRelationResponseDto>>(list);
                }
                else
                {
                    response.Data = null;
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

        public async Task<Response<GetFileResponseDto>> DownloadF1(int idCompany,string language, string format)
        {            
            var response = new Response<GetFileResponseDto>();
            try
            {
                var company = await _companyDomain.GetByIdAsync(idCompany);

                string companyCode = company.OldCode ?? "N" + company.Id.ToString("D6");
                string languageFileName = language == "I" ? "ENG" : "ESP";
                string fileFormat ="{0}_{1}{2}" ;
                string report = language == "I" ? "EIECORE-F1-EMPRESAS" : "EIECORE-F1-EMPRESAS_ES";
                var reportRenderType = StaticFunctions.GetReportRenderType(format);
                var extension = StaticFunctions.FileExtension(reportRenderType);
                var contentType= StaticFunctions.GetContentType(reportRenderType);

                var dictionary = new Dictionary<string, string>
                {
                    { "idCompany", idCompany.ToString() }
                };

                response.Data = new GetFileResponseDto
                {
                    File = await _reportingDownload.GenerateReportAsync(report, reportRenderType, dictionary),
                    ContentType = contentType,
                    Name = string.Format(fileFormat, companyCode, languageFileName, extension)
                };
               
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<GetFileResponseDto>> DownloadF8(int idCompany, string language, string format)
        {
            var response = new Response<GetFileResponseDto>();
            try
            {
                var company = await _companyDomain.GetByIdAsync(idCompany);

                string companyCode = company.OldCode ?? "N" + company.Id.ToString("D6");
                string languageFileName = language == "I" ? "ENG" : "ESP";
                string fileFormat = "{0}_{1}{2}";
                string report = language == "I" ? "F8-EMPRESAS-EN" : "F8-EMPRESAS-ES";
                var reportRenderType = StaticFunctions.GetReportRenderType(format);
                var extension = StaticFunctions.FileExtension(reportRenderType);
                var contentType = StaticFunctions.GetContentType(reportRenderType);

                var dictionary = new Dictionary<string, string>
                {
                    { "idCompany", idCompany.ToString() },
                 };

                response.Data = new GetFileResponseDto
                {
                    File = await _reportingDownload.GenerateReportAsync(report, reportRenderType, dictionary),
                    ContentType = contentType,
                    Name = string.Format(fileFormat, companyCode, languageFileName, extension)
                };

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<GetStatusCompanyResponseDto>> GetStatusCompany(int idCompany)
        {
            var response = new Response<GetStatusCompanyResponseDto>();
            var status = new GetStatusCompanyResponseDto();
            try
            {
                var company = await _companyDomain.GetByIdAsync(idCompany);
                if(company != null)
                {
                    status.Company = true;
                    var background = await _companyBackgroundDomain.GetByIdAsync(idCompany);
                    status.Background = background != null ? true : false;
                    var branch = await _companyBranchDomain.GetByIdAsync(idCompany);
                    status.Branch = branch != null ? true : false;
                    var financial = await _companyFinancialInformationDomain.GetByIdCompany(idCompany);
                    status.Financial = financial != null ? true : false;
                    var balance = await _financialBalanceDomain.GetFinancialBalanceByIdCompany(idCompany, "GENERAL");
                    status.Balance = balance.Count > 0 ? true : false;
                    var sbs = await _companySBSDomain.GetByIdCompany(idCompany);
                    var provider = await _providerDomain.GetProvidersByIdCompany(idCompany);
                    var bankDebt = await _bankDebtDomain.GetBankDebtsByIdCompany(idCompany);
                    var comercial = await _comercialLatePaymentDomain.GetComercialLatePaymetByIdCompany(idCompany);
                    status.Sbs = sbs != null || provider.Count > 0 || bankDebt.Count > 0 || comercial.Count > 0 ? true : false;
                    var opinion = await _companyCreditOpinionDomain.GetByIdCompany(idCompany);
                    status.Opinion = opinion != null ? true : false;
                    var infoGeneral = await _companyGeneralInformationDomain.GetByIdCompany(idCompany);
                    status.InfoGeneral = infoGeneral != null ? true : false;
                    var images = await _companyImagesDomain.GetByIdCompany(idCompany);
                    status.Images = images != null ? true : false;
                }
                else
                {
                    status.Company = false;
                }
                response.Data = status;
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<bool>> NewComercialReferences(int idCompany, int? idTicket)
        {
            var response = new Response<bool>(); 
            try
            {

            }catch(Exception ex)
            {

            }
            return response;
        }
    }
}
