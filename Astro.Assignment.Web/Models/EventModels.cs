using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Astro.Assignment.Web.Models
{
    public class EventModels
    {
        [JsonProperty(PropertyName = "channelId")]
        [Display(Name = "Channel ID")]
        public int ChannelId { get; set; }

        [JsonProperty(PropertyName = "eventId")]
        [Display(Name = "Event ID")]
        public string EventId { get; set; }

        [JsonProperty(PropertyName = "displayDateTime")]
        public DateTime DisplayDateTime { get; set; }

        [JsonProperty(PropertyName = "displayDateTimeUtc")]
        public DateTimeOffset DisplayDateTimeUtc { get; set; }

        [JsonProperty(PropertyName = "displayDuration")]
        public TimeSpan DisplayDuration { get; set; }

        [JsonProperty(PropertyName = "programmeTitle")]
        public string ProgrammeTitle { get; set; }

        [JsonIgnore]
        public DateTimeOffset DisplayEndDateTimeUtc => DisplayDateTimeUtc + DisplayDuration;

        [JsonIgnore]
        public DateTimeOffset DisplayEndDateTime => DisplayDateTime + DisplayDuration;
    }
}