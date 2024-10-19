using bwms_core_domain.AuthorityModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bwms_core_api.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		[HttpPost("Register")]
		public async Task<IActionResult> Register(Employee employee)
		{
			return Ok();
		}

		[HttpGet("GetEmployeeByDeviceId")]
		public async Task<IActionResult> GetEmployeeByDeviceId(string deviceId)
		{
			return Ok(new Employee());
		}
	}
}
