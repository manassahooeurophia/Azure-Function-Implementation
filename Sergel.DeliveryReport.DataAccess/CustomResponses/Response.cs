using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sergel.DeliveryReport.DataAccess.CustomResponses
{
    public class Response<T>
    {
        [JsonProperty(PropertyName= "responseCode")]
        public int ResponseCode { get; set; }
        [JsonProperty(PropertyName = "responseMessage")]
        public string ResponseMessage { get; set; }
        [JsonProperty(PropertyName= "responseData")]
        public T ResponseData { get; set; }
    }
}
