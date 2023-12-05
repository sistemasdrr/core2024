
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
    public class SubscriberPriceApplication : ISubscriberPriceApplication
    {
        private readonly ISubscriberPriceDomain _subscriberPriceDomain;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public SubscriberPriceApplication(ISubscriberPriceDomain subscriberPriceDomain, IMapper mapper, ILogger logger)
        {
            _subscriberPriceDomain = subscriberPriceDomain;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<bool>> AddOrUpdateAsync(AddOrUpdateSubscriberPriceDto obj)
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
                else
                {
                    if (obj.Id > 0)
                    {
                        var existingSubscriberPrice = await _subscriberPriceDomain.GetByIdAsync(obj.Id);
                        if(existingSubscriberPrice != null)
                        {
                            existingSubscriberPrice = _mapper.Map(obj, existingSubscriberPrice);
                            existingSubscriberPrice.UpdateDate = DateTime.Now;
                            response.Data = await _subscriberPriceDomain.UpdateAsync(existingSubscriberPrice);
                        }
                        else
                        {
                            response.Data = false;
                            response.IsSuccess = false;
                        }
                    }
                    else
                    {
                        var newSubscriberPrice = _mapper.Map<SubscriberPrice>(obj);
                        response.Data = await _subscriberPriceDomain.AddAsync(newSubscriberPrice);
                    }
                }
                
            }catch (Exception ex)
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
                var existingSubscriberPrice = await _subscriberPriceDomain.GetByIdAsync(id);
                if(existingSubscriberPrice != null)
                {
                    response.Data = await _subscriberPriceDomain.DeleteAsync(id);
                }
                else
                {
                    response.Data = false;
                    response.IsSuccess = false;
                    response.IsWarning = true;
                }
            }catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }

        public async Task<Response<GetSuscriberPriceByIdResponseDto>> GetSubscriberPriceById(int id)
        {
            var response = new Response<GetSuscriberPriceByIdResponseDto>();
            try
            {
               if(id > 0)
                {
                    var subscriberPrice = await _subscriberPriceDomain.GetByIdAsync(id);
                    response.Data = _mapper.Map<GetSuscriberPriceByIdResponseDto>(subscriberPrice);
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

        public async Task<Response<List<GetSuscriberPriceResponseDto>>> GetSubscriberPricesById(int idSubscriber)
        {
            var response = new Response<List<GetSuscriberPriceResponseDto>>();
            try
            {
                if (idSubscriber > 0)
                {
                    var ListSubscriberPrice = await _subscriberPriceDomain.GetPricesBySubscriberId(idSubscriber);
                    response.Data = _mapper.Map<List<GetSuscriberPriceResponseDto>>(ListSubscriberPrice);
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
