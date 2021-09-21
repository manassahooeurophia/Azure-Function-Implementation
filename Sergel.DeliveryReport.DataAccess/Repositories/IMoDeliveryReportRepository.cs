using Sergel.DeliveryReport.DataAccess.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sergel.DeliveryReport.DataAccess.Repositories
{
    public interface IMoDeliveryReportRepository: ICosmosDbService<MoDeliveryReportModel>
    {
        Task<IEnumerable<MoDeliveryReportModel>> GetMoDeliveryReportAsyn(string gateId, string refId = "", string messageId = "", string shortCode = "", string keyword = "", DateTime? startTime = null, DateTime? endTime = null);
    }
}
