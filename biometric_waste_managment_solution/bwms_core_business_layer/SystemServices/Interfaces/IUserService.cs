using bwms_core_domain.SystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bwms_core_business_layer.SystemServices.Interfaces
{
    public interface IUserService
    {
        Task<bool> CreateUser(User user);
        Task<User> GetUserByUserName(string userName);
    }
}
