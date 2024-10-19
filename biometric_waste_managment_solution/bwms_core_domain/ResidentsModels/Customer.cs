using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bwms_core_domain.SystemModels;

namespace bwms_core_domain.ResidentsModels
{
    public class Customer : User
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public string CustomerPhone { get; set; } = string.Empty;
        public double CustomerLocationLatitude { get; set; }
        public double CustomerLocationLongitude { get; set; }
    }
}
