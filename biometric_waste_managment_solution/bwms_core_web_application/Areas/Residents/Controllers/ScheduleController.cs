using Microsoft.AspNetCore.Mvc;

namespace bwms_core_web_application.Areas.Residents.Controllers
{
    [Area("Residents")]
    public class ScheduleController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
