using bwms_core_domain.SystemModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bwms_core_domain.ResidentsModels
{
    public class WastePickupSchedule
    {
        public int WastePickupScheduleId { get; set; }

        public int CustomerId { get; set; }

        [DisplayName("Scheduled Date")]
        public DateTime ScheduledDate { get; set; }

        [DisplayName("Waste Type")]
        public WasteTypeEnum WasteTypeEnum { get; set; }

        [DisplayName("Pickup Status")]
        public PickupStatusEnum PickupStatusEnum { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; } = string.Empty;

        [DisplayName("Estimated Volume (Kg)")]
        public double EstimatedVolume { get; set; }

        public int? DriverId { get; set; }

        public string? MapLocation { get; set; } = string.Empty;

        #region NAVIGATIONAL PROPERTICE
        public Customer Customer { get; set; } = new Customer();
        #endregion
    }
}
