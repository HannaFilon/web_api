using System;
using Microsoft.Extensions.Caching.Memory;
using Shop.Business.IServices;

namespace Shop.Business.Implementation
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }
        public T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        public void AddToCache<T>(T o, string key)
        {
            if (!_cache.TryGetValue(key, out T cacheEntry))
            {
                cacheEntry = o;
                _cache.Set(key, cacheEntry, TimeSpan.FromMinutes(15));
            }
        }

        public void RemoveFromCache(string key)
        {
            _cache.Remove(key);
        }
    }
}