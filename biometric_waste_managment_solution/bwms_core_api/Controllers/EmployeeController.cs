using bwms_core_business_layer.Interfaces;
using bwms_core_domain.AuthorityModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bwms_core_api.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly IAuthorityService _authorityService;
		public EmployeeController(IAuthorityService authorityService)
		{
			_authorityService = authorityService;

        }

        [HttpPost("Register")]
		public async Task<IActionResult> Register(Employee employee)
		{
            await _authorityService.RegisterEmployee();
            return Ok();
		}

		[HttpGet("GetEmployeeByDeviceId")]
		public async Task<IActionResult> GetEmployeeByDeviceId(string deviceId)
		{
			await _authorityService.AuthenticateEmployee();
			return Ok(new Employee());
		}

        [HttpGet("QRScannerController")]
        public async Task<IActionResult> QRScannerController(string deviceId)
        {
            await _authorityService.AuthenticateEmployee();
            return Ok(new Employee());
        }

    }
}
