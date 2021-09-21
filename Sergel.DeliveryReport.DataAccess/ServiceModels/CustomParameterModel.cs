using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sergel.DeliveryReport.DataAccess.ServiceModels
{
    public class CustomParameterModel
    {
        [JsonProperty(PropertyName= "received")]
        public string Received { get; set; }
    }
}
