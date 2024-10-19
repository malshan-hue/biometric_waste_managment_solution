using bwms_core_domain.SystemModels;
using bwms_core_repository;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using bwms_core_business_layer.Interfaces;

namespace bwms_core_business_layer
{
    public class UserServiceImpl : IUserService
    {
        private readonly IDatabaseService _databaseService;
        public UserServiceImpl(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<bool> CreateUser(User user)
        {
            string jsonString = JsonConvert.SerializeObject(user);
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            var status = dataTransactionManager.UserDataManager.InsertData("CreateUser", jsonString);
            return status;
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            DataTransactionManager dataTransactionManager = new DataTransactionManager(_databaseService.GetConnectionString());
            return dataTransactionManager.UserDataManager.RetrieveData("GetUserByUserName", new SqlParameter[]
            {
                new SqlParameter("@userName", userName)
            }).FirstOrDefault();
        }
    }
}
