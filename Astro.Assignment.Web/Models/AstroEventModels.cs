using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Astro.Assignment.Web.Models
{
    public class AstroEventModels : AstroBaseModels
    {
        public AstroEventModels()
        {
            Events = new List<EventModels>();
        }

        [JsonProperty(PropertyName = "getevent")]
        public IEnumerable<EventModels> Events { get; set; }
    }
}