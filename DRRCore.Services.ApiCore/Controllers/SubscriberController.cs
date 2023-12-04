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

    }
}
