using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Astro.Assignment.Web.FavoriteChannelManagement;
using Astro.Assignment.Web.Models;
using WebGrease.Css.Extensions;

namespace Astro.Assignment.Web.Controllers
{
    public class AstroAssignmentTwoController : AstroAssignmentBaseController
    {
        public AstroAssignmentTwoController()
        {
            FavoriteRepoManagement = new FavoriteRepositoryCache();
        }

        public virtual ActionResult Index()
        {
            // await SS(DateTime.Now, new int[] { 2 });
            return View();
        }

        public async Task<ActionResult> GetChannelsList()
        {
            var result = await GetChannelsMiniInfo(Request.QueryString["order[0][column]"],
                Request.QueryString["order[0][dir]"], Request.QueryString["start"], Request.QueryString["length"]);

            await GetLiveEvents(DateTimeOffset.Parse(Request.QueryString["clientDateTime"]), result);

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private async Task GetLiveEvents(DateTimeOffset clientDateTime, AstroChannelMiniModels source)
        {
            var result = await GetExternalApiData<Models.AstroEventModels>(
                "/ams/v3/getEvents" +
                ConstructUrlParameters(clientDateTime, source.data.Select(st => st.ChannelId).ToArray()));

            foreach (var channel in source.data)
            {
                var liveEvent = result.Events.FirstOrDefault(st =>
                    st.ChannelId == channel.ChannelId && st.DisplayDateTimeUtc <= clientDateTime &&
                    st.DisplayEndDateTimeUtc >= clientDateTime);

                if (liveEvent != null)
                {
                    channel.EventDescription = liveEvent.ProgrammeTitle;
                    channel.StartTime = liveEvent.DisplayDateTimeUtc;
                }
            }
        }

        private string ConstructUrlParameters(DateTimeOffset clientDateTime, int[] channeslId)
        {
            var malaysiaTime = TimeZoneInfo.ConvertTime(clientDateTime, Statics.StaticObjects.MalaysiaTimeZone);

            return string.Format("?channelId={0}&periodStart={1:yyyy-MM-dd HH:mm}&periodEnd={2:yyyy-MM-dd HH:mm}",
                string.Join(",", channeslId.AsEnumerable()), malaysiaTime - new TimeSpan(12, 0, 0),
                malaysiaTime + new TimeSpan(0, 30, 0));
        }
    }
}
