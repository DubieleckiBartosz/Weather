using System.Text.Json.Serialization;

namespace Weather.API.Common.Models.ResponseModels
{
    public class Forecast
    {
        [JsonPropertyName("weather")]
        public Weather[] Weather { get; set; }

        [JsonPropertyName("main")]
        public Main Main { get; set; }

        [JsonPropertyName("clouds")]
        public Clouds Clouds { get; set; }

        [JsonPropertyName("wind")]
        public Wind Wind { get; set; }

        [JsonPropertyName("dt_txt")]
        public string Dt_txt { get; set; }

        [JsonPropertyName("dt")]
        public long Dt { get; set; }
    }
}
