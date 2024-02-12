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
        [Route("abonados")]
        public async Task<ActionResult> MigrarAbonados()
        {
            return Ok(await _migraUser.MigrateSubscriber());
        }
    }
}
