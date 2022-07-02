using System.Text.Json.Serialization;

namespace Weather.API.Common.Models.ResponseModels
{
    public class WeathersClientResponse
    {
        [JsonPropertyName("city")]
        public City City { get; set; }
        [JsonPropertyName("list")]
        public Forecast[] List { get; set; }
    }
}
