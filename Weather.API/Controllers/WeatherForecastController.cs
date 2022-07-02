using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Weather.API.Common.Interfaces;
using Weather.API.Common.Models.RequestModels;

namespace Weather.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherService _weatherService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherService weatherService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _weatherService = weatherService ?? throw new ArgumentNullException(nameof(weatherService));
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetForecasts([FromQuery] WeathersRequest request)//WeatherParameters parameters
        {
            var result = await _weatherService.GetForecastsAsync(request);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetActualWeather([FromQuery] string cityName)
        {
            var result = await _weatherService.GetActualWeatherAsync(cityName);
            return Ok(result);
        }
    }
}
