using bwms_core_domain.ResidentsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bwms_core_business_layer.Interfaces
{
    public interface IAuthorityService
    {
        Task<IEnumerable<WastePickupSchedule>> AuthenticateEmployee();
        Task<IEnumerable<WastePickupSchedule>> UpdateBin();
        Task<IEnumerable<WastePickupSchedule>> RegisterEmployee();
    }
}
