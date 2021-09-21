using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sergel.DeliveryReport.DataAccess.ServiceModels
{
    public class MoDeliveryReportModel
    {
        [JsonProperty(PropertyName = "gateId")]
        public string GateId { get; set; }
        [JsonProperty(PropertyName = "destination")]
        public string Destination { get; set; }
        [JsonProperty(PropertyName = "subNumber")]
        public string SubNumber { get; set; }
        [JsonProperty(PropertyName = "source")]
        public string Source { get; set; }
        [JsonProperty(PropertyName = "content")]
        public ContentModel Content { get; set; }
        [JsonProperty(PropertyName = "operatorValue")]
        public string OperatorValue { get; set; }
        [JsonProperty(PropertyName = "timeStamp")]
        public string TimeStamp { get; set; }
        [JsonProperty(PropertyName = "messageId")]
        public string MessageId { get; set; }
        [JsonProperty(PropertyName = "operatorTimestamp")]
        public string OperatorTimestamp { get; set; }
        [JsonProperty(PropertyName = "route")]
        public RouteModel Route { get; set; }

    }
}
