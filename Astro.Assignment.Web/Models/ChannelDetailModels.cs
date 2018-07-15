using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Astro.Assignment.Web.Models
{
    public class ChannelDetailModels : ChannelModels
    {
        [JsonProperty(PropertyName = "channelDescription")]
        public string ChannelDescription { get; set; }

        [JsonProperty(PropertyName = "channelLanguage")]
        public string ChannelLanguage { get; set; }

        [JsonProperty(PropertyName = "channelColor1")]
        public string ChannelColor1 { get; set; }

        [JsonProperty(PropertyName = "channelColor2")]
        public string ChannelColor2 { get; set; }

        [JsonProperty(PropertyName = "channelColor3")]
        public string ChannelColor3 { get; set; }

        [JsonProperty(PropertyName = "channelCategory")]
        public string ChannelCategory { get; set; }

        [JsonProperty(PropertyName = "channelStbNumber")]
        public new string ChannelStbNumber { get; set; }

        [JsonProperty(PropertyName = "channelHD")]
        public bool? ChannelHd { get; set; }

        [JsonProperty(PropertyName = "hdSimulcastChannel")]
        public int? HdSimulcastChannel { get; set; }

        [JsonProperty(PropertyName = "channelStartDate")]
        public DateTime? ChannelStartDate { get; set; }

        [JsonProperty(PropertyName = "channelEndDate")]
        public DateTime? ChannelEndDate { get; set; }

        [JsonProperty(PropertyName = "channelExtRef")]
        public IEnumerable<ChannelExtRef> ChannelExtRef { get; set; }

        [JsonProperty(PropertyName = "linearOttMapping")]
        public IEnumerable<LinearOttMapping> LinearOttMapping { get; set; }
    }

    public class ChannelExtRef
    {
        [JsonProperty(PropertyName = "system")]
        public string System { get; set; }

        [JsonProperty(PropertyName = "subSystem")]
        public string SubSystem { get; set; }

        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }

    public class LinearOttMapping
    {
        [JsonProperty(PropertyName = "platform")]
        public string Platform { get; set; }
    }
}