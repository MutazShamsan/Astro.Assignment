using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Astro.Assignment.Web.Models
{
    public abstract class AstroBaseModels
    {
        [JsonProperty(PropertyName = "responseMessage")]
        public string ResponseMessage { get; set; }

        [JsonProperty(PropertyName = "responseCode")]
        public string ResponseCode { get; set; }
    }
}