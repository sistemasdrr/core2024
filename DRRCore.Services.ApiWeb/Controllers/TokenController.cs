﻿using DRRCore.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DRRCore.Services.ApiWeb.Controllers
{
    public class TokenController : Controller
    {
       private readonly ITokenGenerator _tokenGenerator;
        public TokenController(ITokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("GetToken")]
        public async Task<ActionResult> getToken()
        {
            return Ok(await _tokenGenerator.GetTokenAsync());
        }
    }
}