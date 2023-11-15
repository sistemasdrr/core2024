using DRRCore.Application.DTO.Core.Request;
using DRRCore.Application.Interfaces.CoreApplication;
using Microsoft.AspNetCore.Mvc;

namespace DRRCore.Services.ApiCore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MasterController : Controller
    {
        private readonly IEmployeeApplication _employeeApplication;
        public MasterController(IEmployeeApplication employeeApplication) {
           _employeeApplication = employeeApplication;
        }

        [HttpPost()]
        [Route("addEmployee")]
        public async Task<ActionResult> AddEmployee(AddOrUpdateEmployeeRequestDto request)
        {
            return Ok(await _employeeApplication.AddOrUpdateAsync(request));
        }
        [HttpPost()]
        [Route("deleteEmployee")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            return Ok(await _employeeApplication.DeleteAsync(id));
        }
        [HttpPost()]
        [Route("activeEmployee")]
        public async Task<ActionResult> ActiveEmployee(int id)
        {
            return Ok(await _employeeApplication.ActiveAsync(id));
        }
        [HttpGet()]
        [Route("getEmployeeById")]
        public async Task<ActionResult> GetEmployee(int id)
        {
            return Ok(await _employeeApplication.GetByIdAsync(id));
        }
        [HttpGet()]
        [Route("getEmployees")]
        public async Task<ActionResult> GetEmployees()
        {
            return Ok(await _employeeApplication.GetAllAsync());
        }
        [HttpGet()]
        [Route("getEmployeesByName")]
        public async Task<ActionResult> GetEmployeesByName(string name)
        {
            return Ok(await _employeeApplication.GetByNameAsync(name));
        }
    }
}
