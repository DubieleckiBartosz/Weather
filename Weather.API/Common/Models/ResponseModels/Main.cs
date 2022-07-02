using System.Text.Json.Serialization;

namespace Weather.API.Common.Models.ResponseModels
{
    public class Main
    {
        [JsonPropertyName("temp")]
        public decimal Temp { get; set; }
    }
}
