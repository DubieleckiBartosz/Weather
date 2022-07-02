using System;
using System.Threading.Tasks;

namespace Weather.API.Common.Interfaces
{
    public interface ICacheService
    {
        Task<T> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T cacheData, TimeSpan? time, TimeSpan? slidingTime);
    }
}
