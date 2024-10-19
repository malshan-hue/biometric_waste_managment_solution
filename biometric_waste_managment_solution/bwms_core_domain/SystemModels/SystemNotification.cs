using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bwms_core_domain.SystemModels
{
    public class SystemNotification
    {
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public string NotificationType { get; set; } = string.Empty;
        public string NotificationPlacement { get; set; } = string.Empty;
    }
}
