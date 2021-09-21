using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sergel.DeliveryReport.DataAccess.ServiceModels
{
    public class DeliveryReportModel
    {
        [JsonProperty(PropertyName ="id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "gateId")]
        public string GateId { get; set; }
        [JsonProperty(PropertyName = "refId")]
        public string RefId { get; set; }
        [JsonProperty(PropertyName = "operatorValue")]
        public string Operatorvalue { get; set; }
        [JsonProperty(PropertyName = "sentTimestamp")]
        public string SentTimestamp { get; set; }
        [JsonProperty(PropertyName = "timestamp")]
        public string Timestamp { get; set; }
        [JsonProperty(PropertyName = "resultCode")]
        public string ResultCode { get; set; }
        [JsonProperty(PropertyName = "operatorResultCode")]
        public string OperatorResultCode { get; set; }
        [JsonProperty(PropertyName = "segments")]
        public string Segments { get; set; }
        [JsonProperty(PropertyName = "customParameters")]
        public CustomParameterModel CustomParameters { get; set; }
        [JsonProperty(PropertyName = "gateCustomParameters")]
        public GateCustomParameterModel GateCustomParameters { get; set; }
    }

}
