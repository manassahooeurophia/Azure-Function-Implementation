using Sergel.DeliveryReport.DataAccess.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sergel.DeliveryReport.DataAccess.Repositories
{
    public interface IDeliveryReportRepository:ICosmosDbService<DeliveryReportModel>
    {
        Task<IEnumerable<DeliveryReportModel>> GetDeliveryReportAsyn(string gateId, string refId = "", string messageId = "", DateTime? startTime = null, DateTime? endTime = null);
    }
}
