using bwms_core_business_layer.Interfaces;
using bwms_core_domain.ResidentsModels;
using bwms_core_repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bwms_core_business_layer
{
    public class AuthorityServiceImpl : IAuthorityService
    {
        private readonly IDatabaseService _databaseService;
        public AuthorityServiceImpl(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<IEnumerable<WastePickupSchedule>> AuthenticateEmployee()
        {
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            return dataTransactionManager.WastePickupScheduleDataManager.RetrieveData("AuthenticateEmployee");
        }

        public async Task<IEnumerable<WastePickupSchedule>> UpdateBin()
        {
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            return dataTransactionManager.WastePickupScheduleDataManager.RetrieveData("UpdateBin");
        }

        public async Task<IEnumerable<WastePickupSchedule>> RegisterEmployee()
        {
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            return dataTransactionManager.WastePickupScheduleDataManager.RetrieveData("RegisterEmployee");
        }
    }
}
