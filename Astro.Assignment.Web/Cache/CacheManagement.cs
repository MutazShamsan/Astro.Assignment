using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace Astro.Assignment.Web.Cache
{
    public static class CacheManagement
    {
        public static async Task<T> GetOrSetAsync<T>(string cacheKey, Func<Task<T>> getItemCallback) where T : class
        {
            var item = GetOnly<T>(cacheKey);
            if (item == null)
            {
                item = await getItemCallback();
                MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddMinutes(10));
            }

            return item;
        }

        private static T GetOnly<T>(string cacheKey) where T : class
        {
            return MemoryCache.Default.Get(cacheKey) as T;
        }
    }
}