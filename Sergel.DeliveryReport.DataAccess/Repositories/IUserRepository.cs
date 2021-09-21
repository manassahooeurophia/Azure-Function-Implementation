using Sergel.DeliveryReport.DataAccess.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sergel.DeliveryReport.DataAccess.Repositories
{
    public interface IUserRepository:ICosmosDbService<UserModel>
    {
        Task<bool> IsUserAuthenticated(String authHeader, string gateId);
        Task<IEnumerable<UserModel>> GetAllUser();
        Task UpdateUser(UserModel userModel);
    }
}
