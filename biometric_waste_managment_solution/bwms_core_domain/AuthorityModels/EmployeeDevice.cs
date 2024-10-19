using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bwms_core_domain.AuthorityModels
{
    public class EmployeeDevice
    {
        public int EmployeeDeviceId { get; set; }
        public int EmployeeId { get; set; }
        public int DeviceId { get; set; }
        public bool IsActive { get; set; }
    }
}
