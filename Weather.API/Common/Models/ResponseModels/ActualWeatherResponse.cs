using System.Text.Json.Serialization;

namespace Weather.API.Common.Models.ResponseModels
{
    public class ActualWeatherResponse : WeatherForecast
    {
        [JsonPropertyName("name")] 
        public string Name { get; set; }
    }
}