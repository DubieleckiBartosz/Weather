using System.Text.Json.Serialization;

namespace Weather.API.Common.Models.ResponseModels
{
    public class City
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }
    }
}
