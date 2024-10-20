using bwms_core_business_layer.Interfaces;
using bwms_core_domain.ResidentsModels;
using bwms_core_domain.SystemModels;
using devspark_core_model.SystemModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace bwms_core_web_application.Areas.Residents.Controllers
{
    [Area("Residents")]
    [Authorize]
    public class ScheduleController : Controller
    {
        private readonly IResidanceService _residanceService;
        public ScheduleController(IResidanceService residanceService)
        {
            _residanceService = residanceService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IQueryable<WastePickupSchedule> wastePickupSchedules = new List<WastePickupSchedule>().AsQueryable();
            var schedules = await _residanceService.GetAllShedules();
            wastePickupSchedules = schedules.AsQueryable();
            return View(wastePickupSchedules.ToList().Count() != 0 ? wastePickupSchedules.ToList() : new List<WastePickupSchedule>());
        }

        [HttpGet]
        public async Task<IActionResult> SheduleModal()
        {
            return ViewComponent("SheduleModal");
        }

        [HttpPost]
        public async Task<IActionResult> CreateSchedule(WastePickupSchedule wastePickupSchedule)
        {
            List<SystemNotification> systemNotifications = new List<SystemNotification>();
            SystemNotification systemNotification = new SystemNotification();

            var status = await _residanceService.CreateSchedule(wastePickupSchedule);

            #region SYSTEM NOTIFICATION

            systemNotification.Title = "Scheduled";
            systemNotification.Message = "Schedule crated successfully";
            systemNotification.Time = DateTime.Now.ToString("dd/MM/yyyy");
            systemNotification.NotificationType = ModelServices.GetEnumDisplayName(NotificationType.Success);
            systemNotification.NotificationPlacement = ModelServices.GetEnumDisplayName(NotificationPlacement.TopRight);

            systemNotifications.Add(systemNotification);

            TempData["SystemNotifications"] = JsonConvert.SerializeObject(systemNotifications);

            #endregion
            return Json(new { status = status });
        }
    }
}
