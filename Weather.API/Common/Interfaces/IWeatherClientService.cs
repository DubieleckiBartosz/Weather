using System.Threading.Tasks;
using Weather.API.Common.Models.ResponseModels;

namespace Weather.API.Common.Interfaces
{
    public interface IWeatherClientService
    {
        //[PollyException(typeof(TimeoutException))]
        Task<WeathersClientResponse> GetWeathersAsync(string countryCode, string cityName);

        Task<WeatherClientResponse> GetActualWeatherDataAsync(string cityName);
    }
}