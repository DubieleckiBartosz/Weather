using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Serilog;
using Weather.API.Common.Interfaces;
using Weather.API.Services;
using Weather.API.Settings;

namespace Weather.API
{
    public static class ClientConfig
    {
        public static IServiceCollection GetHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<WeatherClientSettings>(configuration.GetSection("Weather"));
            services.AddHttpClient<IWeatherClientService, WeatherClientService>();
            services
                .AddHttpClient<IWeatherClientService, WeatherClientService>(c => c.Timeout = TimeSpan.FromSeconds(5))
                .AddPolicyHandler(Policy
                    .HandleResult<HttpResponseMessage>(r =>
                        !r.IsSuccessStatusCode).RetryAsync(3, (de, cnt, c) =>
                    {
                        Log.Error(
                            $"RetryCount: {cnt}, result = {de.Result.StatusCode}. Date-{DateTime.Now}");
                    }));

            return services;
        }
    }
}
