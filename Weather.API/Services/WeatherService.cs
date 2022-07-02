using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Weather.API.Common.Exceptions;
using Weather.API.Common.Interfaces;
using Weather.API.Common.Models.RequestModels;
using Weather.API.Common.Models.ResponseModels;
using Weather.API.Enums;
using Weather.API.Helpers;

namespace Weather.API.Services
{
    public class WeatherService : IWeatherService
    {
        private string cacheWeatherKey = $"WeathersCache-{nameof(WeatherForecastResponse)}";
        private readonly IWeatherClientService _weatherClientService;
        private readonly ICacheService _cacheService;
        private readonly ILogger<WeatherService> _logger;
        private readonly IMapper _mapper;

        public WeatherService(IWeatherClientService weatherClientService, ICacheService cacheService,
            ILogger<WeatherService> logger, IMapper mapper)
        {
            _weatherClientService = weatherClientService;
            _cacheService = cacheService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<WeatherForecastResponse> GetForecastsAsync(WeathersRequest request)
        {
            if (request == null)
            {
                throw new WeatherMyAppException("Request is null!");
            }

            if (string.IsNullOrEmpty(request.CityName) || string.IsNullOrEmpty(request.CountryName))
            {
                throw new WeatherMyAppException("City and country are required!");
            }

            var countryCode = EnumHelpers.GetEnumAttributeValueByString<CountryCodes>(request.CountryName);

            if (countryCode == null)
            {
                throw new WeatherMyAppException($"{request.CountryName} not found in our application");
            }


            var weathersClientResponse = await _cacheService.GetAsync<WeathersClientResponse>(cacheWeatherKey);

            if (weathersClientResponse == null)
            {
                weathersClientResponse =
                    await _weatherClientService.GetWeathersAsync(countryCode, request.CityName);
                if (weathersClientResponse == null || !weathersClientResponse.List.Any())
                {
                    throw new WeatherMyAppException("City not found");
                }

                await _cacheService.SetAsync(cacheWeatherKey, weathersClientResponse,
                    TimeSpan.FromMinutes(24 * 60 * 5), null);
            }

            var resultMap = _mapper.Map<WeatherForecastResponse>(weathersClientResponse);
            return resultMap;
        }

        public async Task<ActualWeatherResponse> GetActualWeatherAsync(string cityName)
        {
            if (string.IsNullOrEmpty(cityName))
            {
                throw new WeatherMyAppException("City is required!");
            }

            var weatherClientResponse =
                await _weatherClientService.GetActualWeatherDataAsync(cityName);
            if (weatherClientResponse == null)
            {
                throw new WeatherMyAppException("City not found");
            }

            var resultMap = _mapper.Map<ActualWeatherResponse>(weatherClientResponse);

            return resultMap;
        }
    }
}
