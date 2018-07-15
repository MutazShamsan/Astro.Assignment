using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Astro.Assignment.Web.Models
{
    public class ChannelModels
    {
        [JsonProperty(PropertyName = "channelId")]
        [Display(Name = "Channel ID")]
        public int ChannelId { get; set; }

        [JsonProperty(PropertyName = "channelTitle")]
        [Display(Name = "Channel Title")]
        public string ChannelTitle { get; set; }

        [JsonProperty(PropertyName = "channelStbNumber")]
        public int ChannelStbNumber { get; set; }

        [JsonIgnore]
        public bool IsFavorite { get; set; }

        [JsonIgnore]
        public string Description { get; set; }

        [JsonIgnore]
        public DateTimeOffset StartTime { get; set; }

        [Display(Name = "Current Event")]
        [JsonIgnore]
        public string EventDescription { get; set; }
    }
}