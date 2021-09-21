using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sergel.DeliveryReport.DataAccess.ServiceModels
{
    public class RouteModel
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
        [JsonProperty(PropertyName = "refId")]
        public string RefId { get; set; }
        [JsonProperty(PropertyName = "gateCustomParameters")]
        public GateCustomParameterModel GateCustomParameters { get; set; }
    }
}
