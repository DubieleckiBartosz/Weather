using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Weather.API.Common.Interfaces;
using Weather.API.Common.Models.ResponseModels;
using Weather.API.Helpers;
using Weather.API.Settings;
using Weather.API.Wrappers;

namespace Weather.API.Services
{
    public class WeatherClientService : IWeatherClientService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<WeatherClientService> _logger;
        private readonly WeatherClientSettings _weatherClientOptions;

        public WeatherClientService(HttpClient httpClient, IOptions<WeatherClientSettings> weatherClientOptions,
            ILogger<WeatherClientService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            _weatherClientOptions = weatherClientOptions.Value;
        }

        public async Task<WeathersClientResponse> GetWeathersAsync(string countryCode, string cityName)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"https://{_weatherClientOptions.ApiHost}/data/2.5/forecast?q={cityName},{countryCode}&appid={_weatherClientOptions.WeatherApiKey}&units=metric");
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsByteArrayAsync();
                var resultError = error.Deserialize<Error>();
                _logger.LogError(
                    $"Phrase: {response.ReasonPhrase}, code: {(int) response.StatusCode}, message:{resultError.Message}.");
                return null;
            }

            var stream = await response.Content.ReadAsStreamAsync();
            return stream.ReadAndDeserializeFromJson<WeathersClientResponse>();
        }

        public async Task<WeatherClientResponse> GetActualWeatherDataAsync(string cityName)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                $"https://{_weatherClientOptions.ApiHost}/data/2.5/weather?q={cityName}&appid={_weatherClientOptions.WeatherApiKey}&units=metric");
            var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsByteArrayAsync();
                var resultError = error.Deserialize<Error>();
                _logger.LogError(
                    $"Phrase: {response.ReasonPhrase}, code: {(int) response.StatusCode}, message:{resultError.Message}.");
                return null;
            }

            var stream = await response.Content.ReadAsStreamAsync();
            return stream.ReadAndDeserializeFromJson<WeatherClientResponse>();
        }
    }
}