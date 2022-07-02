using System.Threading.Tasks;
using Weather.API.Common.Models.RequestModels;
using Weather.API.Common.Models.ResponseModels;

namespace Weather.API.Common.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherForecastResponse> GetForecastsAsync(WeathersRequest request);
        Task<ActualWeatherResponse> GetActualWeatherAsync(string cityName);
    }
}
