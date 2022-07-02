using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weather.API.Settings;

namespace Weather.API.Configurations
{
    public static class CacheConfig
    {
        public static IServiceCollection GetCache(this IServiceCollection services, IConfiguration configuration)
        {
            var cacheSettings = new WeatherCacheSettings();
            configuration.GetSection(WeatherCacheSettings.WeatherSection).Bind(cacheSettings);

            if (!cacheSettings.Enabled)
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                services.AddStackExchangeRedisCache(options =>
                {
                    options.Configuration = cacheSettings.WeatherRedisConnection;
                    options.InstanceName = "Weather";
                });
            }

            return services;
        }
    }
}
