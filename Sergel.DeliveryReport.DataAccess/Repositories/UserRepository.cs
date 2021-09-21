using Sergel.DeliveryReport.DataAccess.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sergel.DeliveryReport.DataAccess.Repositories
{
    public class UserRepository : CosmosDbService<UserModel>, IUserRepository
    {
        public UserRepository(DbConfig dbConfig) : base(dbConfig)
        {

        }

        public async Task<bool> IsUserAuthenticated(string authHeader, string gateId)
        {
            try
            {
                string userName = null;
                string password = null;
                if (authHeader != null && authHeader.StartsWith("Basic"))
                {
                    string encodeduserNameAndPass = authHeader.Substring("Basic ".Length).Trim();                    
                    string usernameAndPassword = Encoding.GetEncoding("UTF-8").GetString(Convert.FromBase64String(encodeduserNameAndPass));
                    int index = usernameAndPassword.IndexOf(":");
                    userName = usernameAndPassword.Substring(0, index);
                    password = usernameAndPassword.Substring(index+1);
                }

                var query = $"Select * from c where c.name=\"{userName}\" and c.pwd=\"{password}\"";
                var result = (await base.GetItemsAsync(query)).FirstOrDefault();
                return result?.GateIDs?.Where(x => x.GateId == gateId).Count() > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UserModel>> GetAllUser()
        {
            try
            {

                var query = $"Select c.id,c.name,c.gateIDs from c";
                var result = (await base.GetItemsAsync(query));
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateUser(UserModel userModel)
        {
            try
            {
               await  base.UpdateItemAsync(userModel.Name, userModel);
            }

            catch (Exception)
            {
                throw;
            }
        }
    }
}
