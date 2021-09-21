using Sergel.DeliveryReport.DataAccess.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sergel.DeliveryReport.DataAccess.Repositories
{
    public class MoDeliveryReportRepository : CosmosDbService<MoDeliveryReportModel>, IMoDeliveryReportRepository
    {
        public MoDeliveryReportRepository(DbConfig dbConfig):base(dbConfig)
        {
              
        }
        public async Task<IEnumerable<MoDeliveryReportModel>> GetMoDeliveryReportAsyn(string gateId, string refId = "", string messageId = "", string shortCode = "", string keyword = "", DateTime? startTime = null, DateTime? endTime = null)
        {
            try
            {
                var baseQuery = $"SELECT * FROM c WHERE c.gateId=\"{gateId}\" and c.messageId=\"{messageId}\"";

                string[] optionalQuery = new string[] { string.IsNullOrEmpty(refId) ==true ? null : $"AND c.route.refId =\"{refId}\"" ,
                                                        string.IsNullOrEmpty(shortCode) ==true ? null: $"AND c.shortCode =\"{shortCode}\"",
                                                        string.IsNullOrEmpty(keyword) ==true ? null: $"AND c.keyword =\"{keyword}\"",
                                                        string.IsNullOrEmpty(startTime.ToString())==true ? null : $"AND c.start >=\"{startTime}\"",
                                                        string.IsNullOrEmpty(endTime.ToString())==true ? null : $"AND c.end <=\"{endTime}\""
                                                      };

                var finalquery = baseQuery+string.Join(" ", optionalQuery.Where(x => x != null));

                return await base.GetItemsAsync(finalquery);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
