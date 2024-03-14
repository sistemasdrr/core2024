﻿using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.Interfaces.MigrationApplication;
using Microsoft.AspNetCore.Mvc;

namespace DRRCore.Services.Migration.Controllers
{
    public class MigrationController : Controller
    {
        private readonly IMigraUser _migraUser;
      
        public MigrationController(IMigraUser migraUser)
        {
            _migraUser = migraUser;            
        }
        [HttpPost()]
        [Route("empresas")]
        public async Task<ActionResult> MigrarEmpresas()
        {
            return Ok(await _migraUser.MigrateCompany());
        }
        [HttpPost()]
        [Route("personas")]
        public async Task<ActionResult> MigrarPersonas()
        {
            return Ok(await _migraUser.MigratePerson());
        }
        [HttpPost()]
        [Route("personaPorCodigo")]
        public async Task<ActionResult> MigrarPersona(string oldCode)
        {
            return Ok(await _migraUser.MigratePersonByOldCode(oldCode));
        }
        [HttpPost()]
        [Route("personaCorreccion")]
        public async Task<ActionResult> personaCorreccion()
        {
            return Ok(await _migraUser.MigratePersonCorreccion());
        }
        [HttpPost()]
        [Route("abonados")]
        public async Task<ActionResult> MigrarAbonados()
        {
            return Ok(await _migraUser.MigrateSubscriber());
        }
        [HttpPost()]
        [Route("oldTicket")]
        public async Task<ActionResult> MigrarOldTicket()
        {
            return Ok(await _migraUser.MigrateOldTicket());
        }
        [HttpPost()]
        [Route("country")]
        public async Task<ActionResult> MigrateCountry()
        {
            return Ok(await _migraUser.MigrateCountry());
        }
        [HttpPost()]
        [Route("subscriberCategory")]
        public async Task<ActionResult> MigrateSubscriberCategory()
        {
            return Ok(await _migraUser.MigrateSubscriberCategory());
        }
    }
}
