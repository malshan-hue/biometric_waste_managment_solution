using bwms_core_domain.ResidentsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bwms_core_business_layer.Interfaces
{
    public interface IResidanceService
    {
        Task<bool> UpdatePaymentStauts(int paymentId);
        Task<IEnumerable<Payment>> GetAllpayments();
        Task<bool> CreateSchedule(WastePickupSchedule wastePickupSchedule);
        Task<IEnumerable<WastePickupSchedule>> GetAllShedules();
    }
}
