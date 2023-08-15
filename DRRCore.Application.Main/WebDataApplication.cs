using AutoMapper;
using DRRCore.Application.DTO.Web;
using DRRCore.Application.Interfaces;
using DRRCore.Domain.Entities.SQLContext;
using DRRCore.Domain.Interfaces;
using DRRCore.Transversal.Common;
using DRRCore.Transversal.Common.Interface;
using System;

namespace DRRCore.Application.Main
{
    public class WebDataApplication : IWebDataApplication
    {
        private readonly IWebDataDomain _webDataDomain;
        private readonly ILogger _logger;
        private IMapper _mapper { get; }
        public WebDataApplication(IWebDataDomain webDataDomain, ILogger logger, IMapper mapper)
        {

            _webDataDomain = webDataDomain;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<Response<bool>> AddOrUpdateWebDataAsync()
        {
            var response = new Response<bool>();
            try
            {
                response.Data = await _webDataDomain.AddOrUpdateWebDataAsync();

            }
            catch (Exception exception)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, exception);
            }
            return response;
        }

        public async Task<Response<WebDataDto>> GetByCodeAsync(string code)
        {
            var response = new Response<WebDataDto>();

            try
            {
                var data = await _webDataDomain.GetByCodeAsync(code);
                if (data != null)
                {
                    response.Data = _mapper.Map<WebDataDto>(data);
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = Messages.MessageNoDataFound;
                    _logger.LogError(response.Message);
                    return response;
                }


            }
            catch (Exception exception)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, exception);
            }
            return response;
        }

        public async Task<Response<List<WebDataDto>>> GetByParamAsync(string param, int page = 1)
        {
            var response = new Response<List<WebDataDto>>();
            if (string.IsNullOrEmpty(param))
            {
                response.IsSuccess = false;
                response.Message = Messages.WhiteParameter;
                _logger.LogAdvertencia(response.Message);
                return response;

            }
            if (param.Trim().Length < 3)
            {
                response.IsSuccess = false;
                response.Message = Messages.ParameterIsNotTooLonger;
                _logger.LogAdvertencia(response.Message);
                return response;

            }
            try
            {
                var data = await _webDataDomain.GetByParamAsync(param.ToUpper(), page);
                response.Data = _mapper.Map<List<WebDataDto>>(data);

            }
            catch (Exception exception)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, exception);
            }
            return response;
        }

        public async Task<Response<List<WebDataDto>>> GetByCountryAndBranchAsync(int country, string branch, int page)
        {
            var response = new Response<List<WebDataDto>>();
            try
            {
                if (country > 0 && !string.IsNullOrEmpty(branch) && page > 0)
                {
                    var data = await _webDataDomain.GetByCountryAndBranchAsync(country, branch, page);
                    response.Data = _mapper.Map<List<WebDataDto>>(data);
                    response.IsSuccess = true;
                    response.IsWarning = false;
                }
                else
                {
                    response.IsSuccess = false;
                    response.IsWarning = true;
                    response.Message = "Error al listar: parametros invalidos";
                    _logger.LogError(response.Message);
                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.IsWarning = true;
                response.Message = "Error al listar: " + ex.Message;
                _logger.LogError(response.Message, ex);
                return response;
            }
        }

        public async Task<Response<List<WebDataDto>>> GetSimilarBrunchAsync(string code)
        {
            var response = new Response<List<WebDataDto>>();
            try
            {
                if (!string.IsNullOrEmpty(code))
                {
                    var data = await _webDataDomain.GetSimilarBrunchAsync(code);
                    response.Data = _mapper.Map<List<WebDataDto>>(data);
                    response.IsSuccess = true;
                    response.IsWarning = false;
                }
                else
                {
                    response.IsSuccess = false;
                    response.IsWarning = true;
                    response.Message = "Error al listar: parametros invalidos";
                    _logger.LogError(response.Message);
                }
                return response;
            }catch (Exception ex)
            {
                response.IsSuccess = false;
                response.IsWarning = true;
                response.Message = "Error al listar: " + ex.Message;
                _logger.LogError(response.Message, ex);
                return response;
            }
        }
    }
}
