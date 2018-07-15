using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Astro.Assignment.Web.Models;

namespace Astro.Assignment.Web.Controllers
{
    public class AstroAssignmentOneController1 : AstroAssignmentBaseController
    {
        public ActionResult Index(string sortByName, string sortingOrder)
        {
            // var result = await GetChannelsMiniInfo(sortByName, sortingOrder);
            return View();
        }

        public async Task<ActionResult> GetMe1()
        {
            //if (Request.QueryString.AllKeys.Length < 4)
            //    return new EmptyResult();
            var ss = User.Identity.Name;

            var result = await GetChannelsMiniInfo(Request.QueryString["order[0][column]"], Request.QueryString["order[0][dir]"], Request.QueryString["start"], Request.QueryString["length"]);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult ToggleChannelFavorit([FromBody] string channelId)
        {
            var tmpChannelId = Convert.ToInt32(channelId);

            if (User.Identity.IsAuthenticated && !string.IsNullOrEmpty(User.Identity.Name))
            {
                using (var dbContext = Models.ApplicationDbContext.Create())
                {
                    var user = dbContext.UserFavoriteChannels.FirstOrDefault(st => st.ChannelId == tmpChannelId && st.UserId == User.Identity.Name);
                    if (user == null)
                    {
                        dbContext.UserFavoriteChannels.Add(new UserFavoriteChannelsModels()
                        {
                            UserId = User.Identity.Name,
                            ChannelId = Convert.ToInt32(channelId)
                        });
                    }
                    else
                        dbContext.UserFavoriteChannels.Remove(user);

                    dbContext.SaveChanges();
                }
            }


            {
                List<int> tmpFav = new List<int>();

                if (Session["FavoriteChannels"] != null)
                {
                    tmpFav = Session["FavoriteChannels"] as List<int>;
                    if (tmpFav.Contains(tmpChannelId))
                        tmpFav.Remove(tmpChannelId);
                    else
                        tmpFav.Add(tmpChannelId);
                }
                else
                {
                    tmpFav.Add(tmpChannelId);
                }

                Session["FavoriteChannels"] = tmpFav;
            }

            return new EmptyResult();
        }

        private async Task<AstroChannelMiniModels> GetChannelsMiniInfo(string sortByName, string sortingOrder, string start, string length)
        {
            AstroChannelMiniModels result = null;

            var tmpResult = await Cache.CacheManagement.GetOrSetAsync<AstroChannelMiniModels>(Constants.Constant.AstroChannelsMiniInfoCachedKey, () => GetExternalApiData<AstroChannelMiniModels>("ams/v3/getChannelList"));
            var resultDetails = await Cache.CacheManagement.GetOrSetAsync<AstroChannelDescriptionModels>(Constants.Constant.AstroChannelFullInfoCachedKey, () => GetExternalApiData<AstroChannelDescriptionModels>("ams/v3/getChannels"));

            if (tmpResult != null)
            {
               // tmpResult.recordsTotal = tmpResult.data.Count();
                var favoriteChannels = GetFavoriteChannels();

                foreach (var item in tmpResult.data)
                {
                    item.IsFavorite = IsFavoriteChannel(item.ChannelId, favoriteChannels);
                    if (resultDetails != null)
                    {
                        var descrRecord = resultDetails.Channels.FirstOrDefault(st => st.ChannelId == item.ChannelId);
                        item.ChannelDescription = descrRecord != null && !string.IsNullOrEmpty(descrRecord.Description) ? descrRecord.Description : "No description found";
                    }
                }


                tmpResult = SortResult(sortByName, sortingOrder, tmpResult);
                result = GetPaginationResult(string.IsNullOrEmpty(start) ? 0 : Convert.ToInt32(start), string.IsNullOrEmpty(length) ? tmpResult.data.Count() : Convert.ToInt32(length), tmpResult);
            }

            return result;
        }


        private AstroChannelMiniModels SortResult(string sortByName, string sortingOrder, AstroChannelMiniModels source)
        {
            switch (sortByName)
            {
                case "0":
                    switch (sortingOrder)
                    {
                        case "asc":
                            source.data = source.data.OrderBy(st => st.ChannelId);
                            break;
                        case "desc":
                            source.data = source.data.OrderByDescending(st => st.ChannelId);
                            break;
                    }
                    break;
                case "1":
                    switch (sortingOrder)
                    {
                        case "asc":
                            source.data = source.data.OrderBy(st => st.ChannelTitle);
                            break;
                        case "desc":
                            source.data = source.data.OrderByDescending(st => st.ChannelTitle);
                            break;
                    }
                    break;
            }

            return source;
        }

        private AstroChannelMiniModels GetPaginationResult(int start, int length, AstroChannelMiniModels source)
        {
            AstroChannelMiniModels result = (AstroChannelMiniModels)source.Clone();
            result.data = new List<ChannelModels>(source.data);
            result.data = result.data.Skip(start).Take(length);

            return result;
        }

        private List<int> GetFavoriteChannels()
        {
            List<int> result = new List<int>();

            if (Session["FavoriteChannels"] != null)
                result = Session["FavoriteChannels"] as List<int>;
            else
            {
                if (User.Identity.IsAuthenticated && !string.IsNullOrEmpty(User.Identity.Name))
                {
                    using (var dbContext = Models.ApplicationDbContext.Create())
                    {
                        result = dbContext.UserFavoriteChannels.Where(st => st.UserId == User.Identity.Name)
                            .Select(st => st.ChannelId).ToList();
                    }

                    Session["FavoriteChannels"] = result;
                }
            }

            return result;
        }

        private bool IsFavoriteChannel(int channelId, List<int> favoriteChannels)
        {
            return favoriteChannels.Contains(channelId);
        }

    }
}
