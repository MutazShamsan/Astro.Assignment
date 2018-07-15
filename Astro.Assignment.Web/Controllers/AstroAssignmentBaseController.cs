using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Astro.Assignment.Web.Models;

namespace Astro.Assignment.Web.Controllers
{
    /// <summary>
    /// Base Controller for all assignments
    /// </summary>
    public class AstroAssignmentBaseController : Controller
    {
        protected readonly HttpClient AstroApi;
        protected AstroChannelMiniModels AstroChannelsMiniInfo { get; set; }
        protected AstroChannelDescriptionModels AstroChannelsDescr { get; set; }
        protected FavoriteChannelManagement.IFavoriteRepository FavoriteRepoManagement { get; set; }

        public AstroAssignmentBaseController()
        {
            AstroApi = new HttpClient
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["AstroWebApiUrl"])
            };
        }

        /// <summary>
        /// Generic Function for any Get request with Json string data
        /// </summary>
        /// <typeparam name="T">The data type expected to return from the API</typeparam>
        /// <param name="apiPath">The API URL</param>
        /// <returns></returns>
        protected async Task<T> GetExternalApiData<T>(string apiPath)
        {
            T result;
            using (var httpResult = await AstroApi.GetAsync(apiPath))
            using (var content = httpResult.Content)
            {
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(await content.ReadAsStringAsync());
            }

            return result;
        }

        protected async Task<AstroChannelMiniModels> GetChannelsMiniInfoGridData(string sortByName, string sortingOrder, int start, int length)
        {
            await GetDataFromExternalApi();
            AddFavoriteAndDescriptionInfo();
            PerformSorting(sortByName, sortingOrder);
            return PerformPagining(start, length);
        }

        private void PerformSorting(string sortByName, string sortingOrder)
        {
            switch (sortByName)
            {
                case "0":
                    switch (sortingOrder)
                    {
                        case "asc":
                            AstroChannelsMiniInfo.data = AstroChannelsMiniInfo.data.OrderBy(st => st.ChannelId);
                            break;
                        case "desc":
                            AstroChannelsMiniInfo.data =
                                AstroChannelsMiniInfo.data.OrderByDescending(st => st.ChannelId);
                            break;
                    }

                    break;
                case "1":
                    switch (sortingOrder)
                    {
                        case "asc":
                            AstroChannelsMiniInfo.data = AstroChannelsMiniInfo.data.OrderBy(st => st.ChannelTitle);
                            break;
                        case "desc":
                            AstroChannelsMiniInfo.data =
                                AstroChannelsMiniInfo.data.OrderByDescending(st => st.ChannelTitle);
                            break;
                    }

                    break;
            }
        }

        private AstroChannelMiniModels PerformPagining(int start, int length)
        {
            if (length < 1)
                return AstroChannelsMiniInfo;

            var result = (AstroChannelMiniModels)AstroChannelsMiniInfo.Clone();
            result.data = new List<ChannelModels>(AstroChannelsMiniInfo.data);
            result.data = result.data.Skip(start).Take(length);

            return result;
        }

        private async Task GetDataFromExternalApi()
        {
            AstroChannelsMiniInfo = await Cache.CacheManagement.GetOrSetAsync<AstroChannelMiniModels>(
                Constants.Constant.AstroChannelsMiniInfoCachedKey,
                () => GetExternalApiData<AstroChannelMiniModels>("ams/v3/getChannelList"));
            AstroChannelsDescr = await Cache.CacheManagement.GetOrSetAsync<AstroChannelDescriptionModels>(
                Constants.Constant.AstroChannelFullInfoCachedKey,
                () => GetExternalApiData<AstroChannelDescriptionModels>("ams/v3/getChannels"));
        }

        private void AddFavoriteAndDescriptionInfo()
        {
            FavoriteRepoManagement.GetFavoriteFromRepo(Constants.Constant.AstroFavoriteChannelsCachedKey);

            foreach (var item in AstroChannelsMiniInfo.data)
            {
                item.IsFavorite = FavoriteRepoManagement.IsFavoriteChannel(item.ChannelId);
                if (AstroChannelsDescr != null)
                {
                    var descrRecord = AstroChannelsDescr.Channels.FirstOrDefault(st => st.ChannelId == item.ChannelId);
                    item.Description = descrRecord != null && !string.IsNullOrEmpty(descrRecord.Description) ? descrRecord.Description : "No description found";
                }
            }
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult ToggleChannelFavorit([FromBody] string channelId)
        {
            FavoriteRepoManagement.UpdateFavoriteRepo(Constants.Constant.AstroFavoriteChannelsCachedKey,
                Convert.ToInt32(channelId));

            return new JsonResult();
        }

        protected async Task<AstroChannelMiniModels> GetChannelsMiniInfo(string sortByName, string sortingOrder,
            string start, string length)
        {
            return await GetChannelsMiniInfoGridData(sortByName, sortingOrder, Convert.ToInt32(start),
                Convert.ToInt32(length));
        }
    }
}