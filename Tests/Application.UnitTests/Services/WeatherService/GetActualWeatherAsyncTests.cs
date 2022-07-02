using AutoFixture;
using System.Threading.Tasks;
using Moq;
using Weather.API.Common.Exceptions;
using Weather.API.Common.Models.ResponseModels;
using Xunit;

namespace Application.UnitTests.Services.WeatherService
{
    public class GetActualWeatherAsyncTests : WeatherServiceBaseTests
    {
        [Fact]
        public async Task Should_Throw_WeatherAppException_When_City_Name_Is_Null_Or_Empty()
        {
            var resultException =
                await Assert.ThrowsAsync<WeatherMyAppException>(() => _service.GetActualWeatherAsync(null));
            Assert.NotNull(resultException);
            Assert.True((int)resultException.StatusCode == 400);
        }

        [Fact]
        public async Task Should_Throw_WeatherAppException_When_Result_From_ClientService_Is_Null()
        {
            _weatherClientServiceMock.Setup(s => s.GetActualWeatherDataAsync(It.IsAny<string>()))
                .ReturnsAsync(() => null);

            var resultException =
                await Assert.ThrowsAsync<WeatherMyAppException>(() => _service.GetActualWeatherAsync(_fixture.Create<string>()));

            _weatherClientServiceMock.Verify(v => v.GetActualWeatherDataAsync(It.IsAny<string>()), Times.Once);
            Assert.NotNull(resultException);
            Assert.True((int)resultException.StatusCode == 400);
        }


        [Fact]
        public async Task Should_Return_ActualWeather()
        {
            _weatherClientServiceMock.Setup(s => s.GetActualWeatherDataAsync(It.IsAny<string>()))
                .ReturnsAsync(_fixture.Build<WeatherClientResponse>().Create());

            var result = await _service.GetActualWeatherAsync(_fixture.Create<string>());

            _weatherClientServiceMock.Verify(v => v.GetActualWeatherDataAsync(It.IsAny<string>()), Times.Once);
            Assert.NotNull(result);
            Assert.IsType<ActualWeatherResponse>(result);
        }
    }
}
