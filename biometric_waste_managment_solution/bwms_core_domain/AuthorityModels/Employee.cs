using bwms_core_domain.SystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bwms_core_domain.AuthorityModels
{
    public class Employee : User
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string EmployeeCode { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        #region NAVIGATIONAL PROPERTIES
        public ICollection<EmployeeDevice> EmployeeDevices { get; set; } = new List<EmployeeDevice>();

        #endregion

    }
}
