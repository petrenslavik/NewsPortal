using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Caching;
using Data_Access_Layer.Entities;

namespace NewsPortal.AppCache
{
    public class CacheManager
    {
        private MemoryCache cache = MemoryCache.Default;

        public NewsItem GetNewsItem(int id)
        {
            return cache.Get(id.ToString()) as NewsItem;
        }

        public bool AddNewsItem(NewsItem value)
        {
            return cache.Add(value.Id.ToString(), value, DateTime.Now.AddMinutes(10));
        }

        public void UpdateNewsItem(NewsItem value)
        {
            cache.Set(value.Id.ToString(), value, DateTime.Now.AddMinutes(10));
        }

        public void DeleteNewsItem(int id)
        {
            if (cache.Contains(id.ToString()))
                cache.Remove(id.ToString());
        }
    }
}