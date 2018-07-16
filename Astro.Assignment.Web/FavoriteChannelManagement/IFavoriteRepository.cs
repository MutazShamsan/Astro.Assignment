using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Astro.Assignment.Web.FavoriteChannelManagement
{
    public interface IFavoriteRepository
    {
        void GetFavoriteFromRepo(string key);
        void UpdateFavoriteRepo(string key, int channelId);
        bool IsFavoriteChannel(int channelId);

        void ClearFavoriteFromRepo(string key);
    }
}
