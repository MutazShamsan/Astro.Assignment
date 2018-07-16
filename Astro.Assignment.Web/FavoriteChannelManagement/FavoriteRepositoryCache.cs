using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Routing;

namespace Astro.Assignment.Web.FavoriteChannelManagement
{
    public class FavoriteRepositoryCache : FavoriteRepositoryBase<MemoryCache>
    {
        public FavoriteRepositoryCache()
        {
            Source = MemoryCache.Default;
        }

        public override void GetFavoriteFromRepo(string key)
        {
            List<int> result = new List<int>();

            lock (Astro.Assignment.Web.Statics.StaticObjects.CacheLockKey)
            {
                if (Source[key] != null)
                    result = Source[key] as List<int>;
            }

            FavoriteChannels = result;
        }

        public override void UpdateFavoriteRepo(string key, int channelId)
        {
            GetFavoriteFromRepo(key);

            if (FavoriteChannels.Contains(channelId))
                FavoriteChannels.Remove(channelId);
            else
                FavoriteChannels.Add(channelId);

            lock (Astro.Assignment.Web.Statics.StaticObjects.CacheLockKey)
            {
                if (Source.Contains(key))
                    Source.Remove(key);

                Source.Add(key, FavoriteChannels, DateTimeOffset.MaxValue);
            }
        }
    }
}