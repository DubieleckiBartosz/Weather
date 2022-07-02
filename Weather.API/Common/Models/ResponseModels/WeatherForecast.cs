namespace Weather.API.Common.Models.ResponseModels
{
    public class WeatherForecast
    {
        public int Clouds { get; set; }
        public string Date { get; set; }
        public int TemperatureC { get; set; }

        public int TemperatureF =>
            32 + (int) (TemperatureC / 0.5556);

        public string Summary { get; set; }
        public decimal Wind { get; set; }
    }
}