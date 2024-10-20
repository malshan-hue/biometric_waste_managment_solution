using Microsoft.AspNetCore.Mvc;

namespace bwms_core_web_application.Controllers
{
    public class PdfTemplatesController : Controller
    {
        public IActionResult TestPdfTemplate()
        {
            return View();
        }

        public IActionResult CustomerInvoiceTemplate()
        {
            return View();
        }
    }
}
