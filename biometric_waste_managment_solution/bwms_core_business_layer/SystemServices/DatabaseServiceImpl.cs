using bwms_core_business_layer.SystemServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bwms_core_business_layer.SystemServices
{
    public class DatabaseServiceImpl : IDatabaseService
    {
        private string _connectionString;

        public DatabaseServiceImpl() { }

        public string GetConnectionString()
        {
            return _connectionString;
        }

        public bool SetConnectionString(string connectionString = "")
        {
            _connectionString = connectionString;
            return true;
        }
    }
}
