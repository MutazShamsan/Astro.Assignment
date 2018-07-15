using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace Astro.Assignment.Web.Cache
{
    /// <summary>
    /// Manage how to get cached data
    /// </summary>
    public static class CacheManagement
    {
        /// <summary>
        /// Get data from main source or from cache
        /// </summary>
        /// <typeparam name="T">Data type of data requested</typeparam>
        /// <param name="cacheKey">Data cached key</param>
        /// <param name="getItemCallback">Fuction to execute if data requested not available in cache</param>
        /// <returns></returns>
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

        /// <summary>
        /// Get data from cache
        /// </summary>
        /// <typeparam name="T">Data type of data requested</typeparam>
        /// <param name="cacheKey">Data cached key</param>
        /// <returns></returns>
        private static T GetOnly<T>(string cacheKey) where T : class
        {
            return MemoryCache.Default.Get(cacheKey) as T;
        }
    }
}