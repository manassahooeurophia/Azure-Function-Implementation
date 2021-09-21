using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sergel.DeliveryReport.DataAccess.ServiceModels
{
    public class GateCustomParameterModel
    {
        [JsonProperty(PropertyName = "GateID")]
        public string GateId { get; set; }
    }
}
