using System.Net.Http;
using System.Threading.Tasks;
using Application.IntegrationTests.Setup;
using Microsoft.AspNetCore.Mvc.Testing;
using Weather.API;
using Weather.API.Common.Models.ResponseModels;
using Xunit;

namespace Application.IntegrationTests.Controllers
{
    public class WeatherControllerTests : BaseSetup
    {
        private const string ActualWeather = "api/WeatherForecast/GetActualWeather";

        private const string GetForecasts = "api/WeatherForecast/GetForecasts";

        public WeatherControllerTests(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_Return_Actual_Forecast()
        {
            var uri = ActualWeather + $"?cityName=Warsaw";
            var responseMessage = await this.ClientCall<object>(null, HttpMethod.Get, uri);
            var responseData = await this.ReadFromResponse<ActualWeatherResponse>(responseMessage);

            Assert.NotNull(responseData);
            Assert.Equal("Warsaw", responseData.Name);
        }

        [Fact]
        public async Task Should_Return_Weathers()
        {
            var uri = GetForecasts + $"?CityName=Warsaw&CountryName=Poland";

            var responseMessage = await this.ClientCall<object>(null, HttpMethod.Get, uri);
            var responseData = await this.ReadFromResponse<WeatherForecastResponse>(responseMessage);

            Assert.NotNull(responseData);
            Assert.True(responseData.WeatherForecasts.Length > 0);
        }
    }
}