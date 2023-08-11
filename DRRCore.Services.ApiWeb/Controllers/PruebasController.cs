using DRRCore.Application.Interfaces;
using DRRCore.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DRRCore.Services.ApiWeb.Controllers
{
    public class PruebasController : Controller
    {
        private readonly IApiUserDomain _userDomain;
        private readonly ITokenValidationApplication _tokenValidation;

        public PruebasController(IApiUserDomain userDomain, ITokenValidationApplication tokenValidation)
        {
            _userDomain = userDomain;
            _tokenValidation = tokenValidation;
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("get")]
        public async Task<ActionResult> getToken(string token)
        {
            return Ok(await _tokenValidation.ValidationTokenAsync(token));
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("post")]
        public async Task<ActionResult> encryptToken(string token)
        {
            return Ok(await _tokenValidation.EncriptTokenAsync(token));
        }
    }
}
