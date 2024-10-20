using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bwms_core_web_application.Areas.Authority.Controllers
{
    [Area("Authority")]
    [Authorize]
    public class CustomersController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
