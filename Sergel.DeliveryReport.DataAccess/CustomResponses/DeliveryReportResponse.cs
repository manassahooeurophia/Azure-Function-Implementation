using Sergel.DeliveryReport.DataAccess.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sergel.DLR.DataAccess.CustomResponses
{
    public class DeliveryReportResponse
    {
        public DeliveryReportResponse()
        {
            this.DeliveryReports = new List<DeliveryReportModel>();
            this.MoDeliveryReports = new List<MoDeliveryReportModel>();
        }
        public IEnumerable<DeliveryReportModel>  DeliveryReports { get; set; }
        public IEnumerable<MoDeliveryReportModel> MoDeliveryReports { get; set; }
    }
}
