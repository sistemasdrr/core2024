using DRRCore.Application.DTO.API;
using DRRCore.Application.Interfaces;
using DRRCore.Domain.Entities.SQLContext;
using DRRCore.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DRRCore.Services.ApiWeb.Controllers
{
    public class PruebasController : Controller
    {
        private readonly IApiUserDomain _userDomain;
        private readonly ITokenValidationApplication _tokenValidation;
        private readonly IApiUserApplication _apiUserApplication;

        public PruebasController(IApiUserDomain userDomain, ITokenValidationApplication tokenValidation, IApiUserApplication apiUserApplication)
        {
            _userDomain = userDomain;
            _tokenValidation = tokenValidation;
            _apiUserApplication = apiUserApplication;
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("validarToken")]
        public async Task<ActionResult> validationTokenAsync()
        {
            return Ok(await _tokenValidation.ValidationTokenAsync());
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("encriptarToken")]
        public async Task<ActionResult> encryptTokenAsync(string token)
        {
            return Ok(await _tokenValidation.EncryptTokenAsync(token));
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("desencriptarToken")]
        public async Task<ActionResult> decryptTokenAsync(string tokenEncriptado)
        {
            return Ok(await _tokenValidation.decryptTokenAsync(tokenEncriptado));
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("insertUser")]
        public async Task<ActionResult> insertUserAsync(ApiUserDataDto dto)
        {
            return Ok(await _apiUserApplication.AddApiUserAsync(dto));
        }
    }
}
