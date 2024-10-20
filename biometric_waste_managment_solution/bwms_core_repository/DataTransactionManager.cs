using bwms_core_domain.ResidentsModels;
using bwms_core_domain.SystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bwms_core_repository
{
    public class DataTransactionManager : IDisposable
    {
        private string _connectionString;

        public DataTransactionManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        #region USER REPOSITORY

        private DataManager<User> _userDataManager;
        public DataManager<User> UserDataManager
        {
            get
            {
                if(this._userDataManager == null)
                {
                    this._userDataManager = new DataManager<User>(_connectionString);
                }

                return this._userDataManager;
            }
        }

        #endregion

        #region RESIDANCE REPOSITORY
        private DataManager<Payment> _paymentDataManager;
        public DataManager<Payment> PaymentDataManager
        {
            get
            {
                if (this._paymentDataManager == null)
                {
                    this._paymentDataManager = new DataManager<Payment>(_connectionString);
                }

                return this._paymentDataManager;
            }
        }

        private DataManager<WastePickupSchedule> _wastePickupScheduleDataManager;
        public DataManager<WastePickupSchedule> WastePickupScheduleDataManager
        {
            get
            {
                if (this._wastePickupScheduleDataManager == null)
                {
                    this._wastePickupScheduleDataManager = new DataManager<WastePickupSchedule>(_connectionString);
                }

                return this._wastePickupScheduleDataManager;
            }
        }
        #endregion

        private bool _disposed = false;
        protected void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {

                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
