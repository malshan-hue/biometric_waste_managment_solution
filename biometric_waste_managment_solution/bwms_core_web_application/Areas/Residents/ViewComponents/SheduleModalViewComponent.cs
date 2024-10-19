using Microsoft.AspNetCore.Mvc;

namespace bwms_core_web_application.Areas.Residents.ViewComponents
{
    public class SheduleModalViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
