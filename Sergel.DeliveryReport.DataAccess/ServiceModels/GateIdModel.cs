using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sergel.DeliveryReport.DataAccess.ServiceModels
{
    public class GateIdModel
    {
        [JsonProperty(PropertyName= "gateId")]
        public string GateId { get; set; }
    }
}
