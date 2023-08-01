﻿using DRRCore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DRRCore.Services.ApiWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WebController : Controller
    {
        private readonly IWebDataApplication _webDataApplication;
        public WebController(IWebDataApplication webDataApplication)
        {         
            _webDataApplication = webDataApplication;
        }

        [HttpGet()]
        [Route("uploadData")]
        public async Task<ActionResult> AddOrUpdateWebData()
        {
            return Ok(await _webDataApplication.AddOrUpdateWebDataAsync());
        }
        [HttpGet()]
        [Route("get/param/{param}/{page}")]
        public async Task<ActionResult> GetByParamPaging(string param,int page=1)
        {           
            return Ok(await _webDataApplication.GetByParamAsync(param,page));
        }
        [HttpGet()]
        [Route("get/code/{code}")]
        public async Task<ActionResult> GetByCode(string code)
        {
            return Ok(await _webDataApplication.GetByCodeAsync(code));
        }
    }
}
