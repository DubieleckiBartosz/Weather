using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using Weather.API.Common.Models.ResponseModels;
using Xunit;
using AutoFixture;

namespace Application.UnitTests.Services.ClientService
{
    public class GetActualWeatherDataAsyncTests : ClientServiceBaseTests
    {
        [Fact]
        public async Task Should_Return_Weather_Type_When_City_Found()
        {
            var responseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                ReasonPhrase = It.IsAny<string>(),
                Content = new StringContent(
                    "{\"location\": {\"name\": \"city\"}}"
                )
            };
            GetHttpMessageHandlerMock(responseMessage);

            var weatherApiClient = await _service.GetActualWeatherDataAsync(_fixture.Create<string>());

            VerifyLogError(Times.Never());
            Assert.NotNull(weatherApiClient);
            Assert.IsType<WeatherClientResponse>(weatherApiClient);
        }

        [Fact]
        public async Task Should_Return_Null_When_City_NotFound()
        {
            var responseError = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                ReasonPhrase = It.IsAny<string>(),
                Content = new StringContent(
                    "{\"error\":{\"message\":\"error\"}}"
                )
            };
            GetHttpMessageHandlerMock(responseError);

            var resultNull = await _service.GetActualWeatherDataAsync(_fixture.Create<string>());
            VerifyLogError(Times.Once());
            Assert.Null(resultNull);
        }
    }
}
