using Microsoft.Extensions.DependencyInjection;
using Weather.API.Common.Interfaces;
using Weather.API.Services;

namespace Weather.API.Configurations
{
    public static class DependencyInjection
    {
        public static IServiceCollection GetAppDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<IWeatherService, WeatherService>();
            services.AddSingleton<ICacheService, CacheService>();
            return services;
        }
    }
}
