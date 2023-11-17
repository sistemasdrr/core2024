using AutoMapper;
using DRRCore.Application.DTO.Core.Request;
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
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CompanyApplication(ICompanyDomain companyDomain,IMapper mapper,ILogger logger)
        {
            _companyDomain = companyDomain;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Response<bool>> AddOrUpdateAsync(AddOrUpdateCompanyRequestDto obj)
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
                            IdLanguage=1,
                            LastUpdaterUser=1
                        });
                    }
                    var newCompany = _mapper.Map<Company>(obj);
                    newCompany.Traductions = traductions;
                     response.Data = await _companyDomain.AddAsync(newCompany);
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
                    response.Data = await _companyDomain.UpdateAsync(existingCompany);
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
    }
}
