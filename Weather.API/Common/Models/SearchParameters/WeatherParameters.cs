using System;

namespace Weather.API.Common.Models.SearchParameters
{
    public class WeatherParameters
    {
        public DateTime? Time { get; set; }
        public string CityName { get; set; }
        public string CountryShort { get; set; }
    }
}
