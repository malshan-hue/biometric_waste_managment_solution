using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bwms_core_business_layer.SystemServices.Interfaces
{
    public interface IDatabaseService
    {
        bool SetConnectionString(string connectionString = "");
        string GetConnectionString();
    }
}
