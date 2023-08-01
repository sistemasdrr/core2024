using DRRCore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace DRRCore.Services.ApiEmail.Controllers
{
    [Route("[controller]")]
    public class EmailController : Controller
    {
        private readonly IEmailUserApplication _emailUserApplication;

        /// <summary>
        /// Constructor de la controlador
        /// </summary>     
        public EmailController(IEmailUserApplication emailUserApplication)
        {
            _emailUserApplication = emailUserApplication;
        }
        [HttpPost()]
        [Route("api/addUser")]
        public async Task<ActionResult> AddAsync()
        {
           return Ok(_emailUserApplication.Add());
        }
    }
}
