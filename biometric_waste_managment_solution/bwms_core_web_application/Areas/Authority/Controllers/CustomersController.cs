using bwms_core_domain.SystemModels;
using devspark_core_business_layer.SystemService.Interfaces;
using devspark_core_model.SystemModels;
using devspark_core_web.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Stripe;

namespace bwms_core_web_application.Areas.Authority.Controllers
{
    [Area("Authority")]
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly IMailService _mailService;
        public CustomersController(IMailService mailService)
        {
            _mailService = mailService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> DownloadInvoice(string courseId)
        {

            string htmlContent = await ITextSharpPdfHelper.RenderViewToStringAsync(this, "CustomerInvoiceTemplate", new Customer());

            byte[] pdfBytes = ITextSharpPdfHelper.GeneratePdfFromHtml(htmlContent);

            var pdfName = "Invoice.pdf";

            // Return the PDF as a file download
            return File(pdfBytes, "application/pdf", pdfName);
        }

        [HttpPost]
        public async Task<IActionResult> EmailInvoice()
        {
            List<SystemNotification> systemNotifications = new List<SystemNotification>();
            SystemNotification systemNotification = new SystemNotification();

            var emailBody = await EmailTemplateHelper.GenerateEmailBody(this, "CustomerInvoiceTemplate", new Customer());

            await _mailService.SendGoogleMail("malshan.edu@gmail.com", "October Invoice", emailBody);

            #region SYSTEM NOTIFICATION

            systemNotification.Title = "Mail Success";
            systemNotification.Message = "Mail Sent successfully";
            systemNotification.Time = DateTime.Now.ToString("dd/MM/yyyy");
            systemNotification.NotificationType = ModelServices.GetEnumDisplayName(NotificationType.Success);
            systemNotification.NotificationPlacement = ModelServices.GetEnumDisplayName(NotificationPlacement.TopRight);

            systemNotifications.Add(systemNotification);

            TempData["SystemNotifications"] = JsonConvert.SerializeObject(systemNotifications);

            #endregion

            return RedirectToAction("Index", "Customers", new { Area = "Authority" });
        }
    }
}
