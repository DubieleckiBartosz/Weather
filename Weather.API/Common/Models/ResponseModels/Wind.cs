using System.Text.Json.Serialization;

namespace Weather.API.Common.Models.ResponseModels
{
    public class Wind
    {
        [JsonPropertyName("speed")]
        public decimal Speed { get; set; }
    }
}
