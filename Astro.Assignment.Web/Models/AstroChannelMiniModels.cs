using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Astro.Assignment.Web.Models
{
    public class AstroChannelMiniModels : AstroBaseModels, ICloneable
    {
        public AstroChannelMiniModels()
        {
            data = new List<ChannelModels>();
        }

        [JsonProperty(PropertyName = "channels")]
        public IEnumerable<ChannelModels> data { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}