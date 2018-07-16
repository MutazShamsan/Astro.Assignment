using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Astro.Assignment.Web.Models
{
    public class EventModels
    {
        private DateTimeOffset _startDateTimeUtc;
        private DateTimeOffset _startDateTime;

        [JsonProperty(PropertyName = "channelId")]
        [Display(Name = "Channel ID")]
        public int ChannelId { get; set; }

        [JsonProperty(PropertyName = "eventId")]
        [Display(Name = "Event ID")]
        public string EventId { get; set; }

        [JsonProperty(PropertyName = "displayDateTime")]
        public DateTimeOffset DisplayDateTime
        {
            get { return _startDateTime; }
            set
            {
                _startDateTime = new DateTimeOffset(value.DateTime, Statics.StaticObjects.MalaysiaTimeZone.GetUtcOffset(value.DateTime));
            }
        }

        [JsonProperty(PropertyName = "displayDateTimeUtc")]
        public DateTimeOffset DisplayDateTimeUtc
        {
            get { return _startDateTimeUtc; }
            set { _startDateTimeUtc = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second, DateTimeKind.Utc); }
        }

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