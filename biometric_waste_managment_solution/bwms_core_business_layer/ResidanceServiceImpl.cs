using bwms_core_business_layer.Interfaces;
using bwms_core_domain.ResidentsModels;
using bwms_core_repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bwms_core_business_layer
{
    public class ResidanceServiceImpl : IResidanceService
    {
        private readonly IDatabaseService _databaseService;
        public ResidanceServiceImpl(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<bool> UpdatePaymentStauts(int paymentId)
        {
            var jsonString = JsonConvert.SerializeObject(paymentId);
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            return dataTransactionManager.PaymentDataManager.UpdateData("UpdatePaymentStauts", jsonString);
        }

        public async Task<IEnumerable<Payment>> GetAllpayments()
        {
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            return dataTransactionManager.PaymentDataManager.RetrieveData("GetAllpayments");
        }

        public async Task<bool> CreateSchedule(WastePickupSchedule wastePickupSchedule)
        {
            var jsonString = JsonConvert.SerializeObject(wastePickupSchedule);
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            return dataTransactionManager.WastePickupScheduleDataManager.InsertData("CreateSchedule", jsonString);
        }

        public async Task<IEnumerable<WastePickupSchedule>> GetAllShedules()
        {
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            return dataTransactionManager.WastePickupScheduleDataManager.RetrieveData("GetAllShedules");
        }
    }
}
