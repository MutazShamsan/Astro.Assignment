using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Astro.Assignment.Web.FavoriteChannelManagement
{
    public abstract class FavoriteRepositoryBase<T> : IFavoriteRepository
    {
        protected List<int> FavoriteChannels { get; set; }
        protected T Source { get; set; }

        public abstract void GetFavoriteFromRepo(string key);
        public abstract void UpdateFavoriteRepo(string key, int channelId);

        public virtual bool IsFavoriteChannel(int channelId)
        {
            return FavoriteChannels.Contains(channelId);
        }
    }
}