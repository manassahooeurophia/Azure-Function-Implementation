using Microsoft.Azure.Cosmos;
using Sergel.DeliveryReport.DataAccess.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sergel.DeliveryReport.DataAccess.Repositories
{
    public class DeliveryReportRepository : CosmosDbService<DeliveryReportModel>, IDeliveryReportRepository
    {
        public DeliveryReportRepository(
            DbConfig dbConfig) : base(dbConfig)
        {

        }

        public async Task<IEnumerable<DeliveryReportModel>> GetDeliveryReportAsyn(string gateId, string refId = "", string messageId="", DateTime? startTime = null, DateTime? endTime = null)
        {
            try
            {
                var baseQuery = $"SELECT * FROM c WHERE c.gateId=\"{gateId}\"";

                string[] optionalQuery = new string[] { string.IsNullOrEmpty(refId) ==true ? null : $"AND c.refId =\"{refId}\"" ,
                                                        string.IsNullOrEmpty(messageId) ==true ? null: $"AND c.messageId =\"{messageId}\"",
                                                        string.IsNullOrEmpty(startTime.ToString())==true ? null : $"AND c.start >=\"{startTime}\"",
                                                        string.IsNullOrEmpty(endTime.ToString())==true ? null : $"AND c.end <=\"{endTime}\""
                                                      };

                var finalquery = baseQuery + string.Join(" ", optionalQuery.Where(x => x != null));

                return await base.GetItemsAsync(finalquery);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}