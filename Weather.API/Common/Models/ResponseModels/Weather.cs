using System.Text.Json.Serialization;

namespace Weather.API.Common.Models.ResponseModels
{
    public class Weather
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
