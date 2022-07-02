using System;
using System.Threading.Tasks;
using Moq;
using Weather.API.Common.Exceptions;
using Weather.API.Common.Models.RequestModels;
using Weather.API.Common.Models.ResponseModels;
using Xunit;
using AutoFixture;

namespace Application.UnitTests.Services.WeatherService
{
    public class GetForecastsAsyncTests : WeatherServiceBaseTests
    {
        [Fact]
        public async Task Should_Throw_WeatherMyAppException_When_Request_Is_Null()
        {
            var resultException =
                await Assert.ThrowsAsync<WeatherMyAppException>(() => _service.GetForecastsAsync(null));

            Assert.NotNull(resultException);
            Assert.True((int)resultException.StatusCode == 400);
            Assert.True(resultException.Message == "Request is null!");
        }

        [Fact]
        public async Task Should_Throw_WeatherMyAppException_When_CityName_Is_Null()
        {
            var request = _fixture.Build<WeathersRequest>().Without(w => w.CityName).Create();
            var resultException =
                await Assert.ThrowsAsync<WeatherMyAppException>(() => _service.GetForecastsAsync(request));

            Assert.NotNull(resultException);
            Assert.True((int)resultException.StatusCode == 400);
            Assert.True(resultException.Message == "City and country are required!");
        }

        [Fact]
        public async Task Should_Throw_WeatherMyAppException_When_CountryName_Is_Null()
        {
            var request = _fixture.Build<WeathersRequest>().Without(w => w.CountryName).Create();
            var resultException =
                await Assert.ThrowsAsync<WeatherMyAppException>(() => _service.GetForecastsAsync(request));

            Assert.NotNull(resultException);
            Assert.True((int)resultException.StatusCode == 400);
            Assert.True(resultException.Message == "City and country are required!");
        }

        [Fact]
        public async Task Should_Throw_WeatherMyAppException_When_CountryCode_NotFound()
        {
            var request = _fixture.Build<WeathersRequest>().Create();
            var resultException =
                await Assert.ThrowsAsync<WeatherMyAppException>(() => _service.GetForecastsAsync(request));

            Assert.NotNull(resultException);
            Assert.True((int)resultException.StatusCode == 400);
            Assert.True(resultException.Message == $"{request.CountryName} not found in our application");
        }

        [Fact]
        public async Task Should_Take_Data_From_The_CacheService()
        {
            var request = _fixture.Build<WeathersRequest>().With(w => w.CountryName, GetCountry())
                .Create();

            _cacheServiceMock.Setup(s => s.GetAsync<WeathersClientResponse>(It.IsAny<string>()))
                .ReturnsAsync(_fixture.Build<WeathersClientResponse>().Create());

            var result = await _service.GetForecastsAsync(request);
            _cacheServiceMock.Verify(
                v => v.SetAsync(It.IsAny<string>(), It.IsAny<WeathersClientResponse>(), It.IsAny<TimeSpan?>(), null),
                Times.Never);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Should_Throw_Exception_When_City_Not_Found()
        {
            var request = _fixture.Build<WeathersRequest>().With(w => w.CountryName, GetCountry())
                .Create();

            _cacheServiceMock.Setup(s => s.GetAsync<WeathersClientResponse>(It.IsAny<string>()))
                .ReturnsAsync(() => null);
            _weatherClientServiceMock.Setup(s => s.GetWeathersAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(() => null);

            var resultError = await Assert.ThrowsAsync<WeatherMyAppException>(() => _service.GetForecastsAsync(request));

            _cacheServiceMock.Verify(
                v => v.SetAsync(It.IsAny<string>(), It.IsAny<WeathersClientResponse>(), It.IsAny<TimeSpan?>(), null),
                Times.Never);
            Assert.NotNull(resultError);
        }

        [Fact]
        public async Task Should_Save_Data_In_Cache()
        {
            var request = _fixture.Build<WeathersRequest>()
                .With(w => w.CountryName, GetCountry())
                .Create();

            _cacheServiceMock.Setup(s => s.GetAsync<WeathersClientResponse>(It.IsAny<string>()))
                .ReturnsAsync(() => null);
            _weatherClientServiceMock.Setup(s => s.GetWeathersAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(_fixture.Build<WeathersClientResponse>().Create());

            var result = _service.GetForecastsAsync(request);

            _cacheServiceMock.Verify(v => v.SetAsync(It.IsAny<string>(),
                    It.IsAny<WeathersClientResponse>(), It.IsAny<TimeSpan?>(), null),
                Times.Once);
            Assert.NotNull(result);
        }
    }
}
