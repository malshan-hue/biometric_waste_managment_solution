using Microsoft.AspNetCore.Mvc;

namespace bwms_core_web_application.Controllers
{
    public class EmailTemplatesController : Controller
    {
        public IActionResult TestEmailTemplate()
        {
            return View();
        }
    }
}
