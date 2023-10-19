using Microsoft.AspNetCore.Mvc;

namespace DRRCore.Services.Migration.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        public UsersController() { 
        
        }
        [HttpPost()]
        [Route("migrarUsuarios")]
        public async Task<ActionResult> MigrateUsers()
        {
           throw new NotImplementedException();
        }
       
    }
}
