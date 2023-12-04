using AutoMapper;
using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.Interfaces.CoreApplication;
using DRRCore.Domain.Entities.SqlCoreContext;
using DRRCore.Domain.Interfaces.CoreDomain;
using DRRCore.Transversal.Common;
using DRRCore.Transversal.Common.Interface;

namespace DRRCore.Application.Main.CoreApplication
{
    public class SubscriberApplication : ISubscriberApplication
    {
        private readonly ISubscriberDomain _subscriberDomain;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public SubscriberApplication(ISubscriberDomain subscriberDomain, IMapper mapper, ILogger logger)
        {
            _subscriberDomain = subscriberDomain;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<int>> AddOrUpdateAsync(AddOrUpdateSubscriberRequestDto subscriberDto)
        {
            var response = new Response<int>();
            try
            {
                if (subscriberDto == null)
                {
                    response.IsSuccess = false;
                    response.Message = Messages.WrongParameter;
                    _logger.LogError(response.Message);
                    return response;
                }
                if (subscriberDto.Id == 0)
                {
                    var newSubscriber = _mapper.Map<Subscriber>(subscriberDto);
                    response.Data = await _subscriberDomain.AddSubscriberAsync(newSubscriber);
                }
                else
                {
                    var existingSubscriber = await _subscriberDomain.GetSubscriberById(subscriberDto.Id);
                    if (existingSubscriber == null)
                    {
                        response.IsSuccess = false;
                        response.Message = Messages.MessageNoDataFoundEmployee;
                        _logger.LogError(response.Message);
                        return response;
                    }
                    existingSubscriber = _mapper.Map(subscriberDto, existingSubscriber);
                    existingSubscriber.UpdateDate = DateTime.Now;
                    await _subscriberDomain.UpdateSubscriberAsync(existingSubscriber);
                    response.Data = existingSubscriber.Id;
                }

            }catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = Messages.BadQuery;
                _logger.LogError(response.Message, ex);
            }
            return response;
        }
        public Task<Response<List<Subscriber>>> GetSubscriber(string code, string name, bool enable)
        {
            throw new NotImplementedException();
        }

        public Task<Response<Subscriber>> GetSubscriberById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
