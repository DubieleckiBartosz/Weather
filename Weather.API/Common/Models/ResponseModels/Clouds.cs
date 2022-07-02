using System.Text.Json.Serialization;

namespace Weather.API.Common.Models.ResponseModels
{
    public class Clouds
    {
        [JsonPropertyName("all")]
        public int All { get; set; }
    }
}
