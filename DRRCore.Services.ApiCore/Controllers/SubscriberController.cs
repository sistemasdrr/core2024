using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.Interfaces.CoreApplication;
using DRRCore.Application.Main.CoreApplication;
using Microsoft.AspNetCore.Mvc;

namespace DRRCore.Services.ApiCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriberController : Controller
    {
        public readonly ISubscriberApplication _subscriberApplication;
        public SubscriberController(ISubscriberApplication subscriberApplication)
        {
            _subscriberApplication = subscriberApplication;
        }

        [HttpPost()]
        [Route("add")]
        public async Task<ActionResult> AddSubscriber(AddOrUpdateSubscriberRequestDto request)
        {
            return Ok(await _subscriberApplication.AddOrUpdateAsync(request));
        }
        [HttpGet()]
        [Route("get")]
        public async Task<ActionResult> GetSubscribers(string? code, string? name, string? enable)
        {
            code ??= string.Empty;
            name ??= string.Empty;
            enable ??= string.Empty;
            return Ok(await _subscriberApplication.GetSubscriber(code, name, enable));
        }
        [HttpGet()]
        [Route("getById")]
        public async Task<ActionResult> GetSubscriberById(int id)
        {
            return Ok(await _subscriberApplication.GetSubscriberById(id));
        }
        [HttpPost()]
        [Route("delete")]
        public async Task<ActionResult> DeleteSubscriber(int id)
        {
            return Ok(await _subscriberApplication.DeleteSubscriber(id));
        }
        [HttpPost()]
        [Route("active")]
        public async Task<ActionResult> ActiveSubscriber(int id)
        {
            return Ok(await _subscriberApplication.ActiveSubscriber(id));
        }
    }
}
