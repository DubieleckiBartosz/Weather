using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Weather.API.Common.Interfaces;
using Weather.API.Helpers;
using Weather.API.Settings;

namespace Weather.API.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly WeatherCacheSettings _weatherCacheSettings;

        public CacheService(IDistributedCache distributedCache, IOptions<WeatherCacheSettings> weatherSettings)
        {
            _distributedCache = distributedCache;
            _weatherCacheSettings = weatherSettings.Value;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var resultCache = await _distributedCache.GetAsync(key);
            return resultCache?.Length > 0 ? resultCache.Deserialize<T>() : default;
        }

        public async Task SetAsync<T>(string key, T cacheData, TimeSpan? time, TimeSpan? slidingTime)
        {
            if (key == null || cacheData == null)
            {
                throw new ArgumentNullException();
            }

            var cacheEntryOptions = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(time == null
                    ? TimeSpan.FromMinutes(_weatherCacheSettings.DefaultTimeInMinutes)
                    : (TimeSpan)time)
                .SetSlidingExpiration(slidingTime ?? TimeSpan.FromMinutes(1));
            await _distributedCache.SetAsync(key, cacheData.Serialize(), cacheEntryOptions);
        }
    }
}
