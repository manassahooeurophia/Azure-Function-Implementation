using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sergel.DeliveryReport.DataAccess.ServiceModels
{
    public class ContentModel
    {
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }
        [JsonProperty(PropertyName = "userData")]
        public string UserData { get; set; }
        [JsonProperty(PropertyName = "encoding")]
        public string Encoding { get; set; }
    }
}
