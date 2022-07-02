namespace Weather.API.Common.Models.ResponseModels
{
    public class WeatherForecastResponse
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public WeatherForecast[] WeatherForecasts { get; set; }
    }
}