using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Astro.Assignment.Web.Models
{
    public class AstroChannelDescriptionModels : AstroBaseModels
    {
        public AstroChannelDescriptionModels()
        {
            Channels = new List<ChannelDescriptionModels>();
        }

        [JsonProperty(PropertyName = "channel")]
        public IEnumerable<ChannelDescriptionModels> Channels { get; set; }
    }

    public class ChannelDescriptionModels
    {
        [JsonProperty(PropertyName = "channelId")]
        public int ChannelId { get; set; }

        [JsonProperty(PropertyName = "channelDescription")]
        public string Description { get; set; }
    }
}