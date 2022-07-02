namespace Weather.API.Settings
{
    public class WeatherCacheSettings
    {
        public const string WeatherSection = "WeatherCacheSettings";
        public bool Enabled { get; set; }
        public string WeatherRedisConnection { get; set; }
        public int DefaultTimeInMinutes { get; set; }
    }
}
