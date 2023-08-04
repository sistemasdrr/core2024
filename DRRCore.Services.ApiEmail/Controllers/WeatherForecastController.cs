using DRRCore.Transversal.Common.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DRRCore.Services.ApiEmail.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IMailSender _mailSender;
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,IMailSender mailSender)
        {
            _logger = logger;
            _mailSender = mailSender;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpPost()]
        [Route("api/send")]
        public async Task<ActionResult> Send()
        {
            for (int i = 0; i < 2500; i++)
            {
                await _mailSender.SendMailAsync(new Transversal.Common.EmailValues
                {
                    FromEmail = "pedido@informesdrr.com",
                    ToEmail = "delriscoprueba@gmail.com",
                    Subject = "Probando Correo N° "+(i+1),
                    Body="Esto es prueba de correo N° "+(i + 1),
                    IsHtml = false
                });
            }
            ;
            return Ok();
        }
    }
}