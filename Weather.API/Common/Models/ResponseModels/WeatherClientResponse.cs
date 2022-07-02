using System.Text.Json.Serialization;

namespace Weather.API.Common.Models.ResponseModels
{
    public class WeatherClientResponse
    {
        [JsonPropertyName("main")]
        public Main Main { get; set; }

        [JsonPropertyName("clouds")]
        public Clouds Clouds { get; set; }

        [JsonPropertyName("wind")]
        public Wind Wind { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("weather")]
        public Weather[] Weather { get; set; }
    }
}
