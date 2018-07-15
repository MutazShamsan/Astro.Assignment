using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Astro.Assignment.Web.Models;

namespace Astro.Assignment.Web.FavoriteChannelManagement
{
    public class FavoriteRepositoryDatabase : FavoriteRepositoryBase<ApplicationDbContext>
    {
        public FavoriteRepositoryDatabase(ApplicationDbContext source)
        {
            Source = source;
        }

        public override void GetFavoriteFromRepo(string key)
        {
            FavoriteChannels = Source.UserFavoriteChannels.Where(st => st.UserId == key).Select(st => st.ChannelId).ToList();
        }

        public override void UpdateFavoriteRepo(string key, int channelId)
        {
            var user = Source.UserFavoriteChannels.FirstOrDefault(st => st.ChannelId == channelId && st.UserId == key);
            if (user == null)
            {
                Source.UserFavoriteChannels.Add(new UserFavoriteChannelsModels()
                {
                    UserId = key,
                    ChannelId = channelId
                });
            }
            else
                Source.UserFavoriteChannels.Remove(user);

            Source.SaveChanges();
        }
    }
}