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
            var userFavorites = Source.UserFavoriteChannels.FirstOrDefault(st => st.ChannelId == channelId && st.UserId == key);
            if (userFavorites == null)
            {
                Source.UserFavoriteChannels.Add(new UserFavoriteChannelsModels()
                {
                    UserId = key,
                    ChannelId = channelId
                });
            }
            else
                Source.UserFavoriteChannels.Remove(userFavorites);

            Source.SaveChanges();
        }

        public override void ClearFavoriteFromRepo(string key)
        {
            var userFavorites = Source.UserFavoriteChannels.Where(st => st.UserId == key).ToList();
            Source.UserFavoriteChannels.RemoveRange(userFavorites);

            Source.SaveChanges();
        }
    }
}