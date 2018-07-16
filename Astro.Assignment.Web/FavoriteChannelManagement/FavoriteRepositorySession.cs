using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Astro.Assignment.Web.FavoriteChannelManagement
{
    public class FavoriteRepositorySession : FavoriteRepositoryBase<System.Web.SessionState.HttpSessionState>
    {
        public FavoriteRepositorySession()
        {
            Source = System.Web.HttpContext.Current.Session;
        }

        public override void GetFavoriteFromRepo(string key)
        {
            List<int> result = new List<int>();

            if (Source[key] != null)
                result = Source[key] as List<int>;

            FavoriteChannels = result;
        }

        public override void UpdateFavoriteRepo(string key, int channelId)
        {
            GetFavoriteFromRepo(key);

            if (FavoriteChannels.Contains(channelId))
                FavoriteChannels.Remove(channelId);
            else
                FavoriteChannels.Add(channelId);

            Source[key] = FavoriteChannels;
        }

        public override void ClearFavoriteFromRepo(string key)
        {
            Source.Remove(key);
        }
    }
}