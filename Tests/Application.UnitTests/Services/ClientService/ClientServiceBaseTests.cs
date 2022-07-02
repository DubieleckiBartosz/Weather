using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.AutoMock;
using Moq.Protected;
using Weather.API.Services;
using Weather.API.Settings;

namespace Application.UnitTests.Services.ClientService
{
    public abstract class ClientServiceBaseTests
    {
        protected WeatherClientSettings _clientSettings;
        protected Mock<IOptions<WeatherClientSettings>> _optionsClientSettingsMock;
        protected Mock<ILogger<WeatherClientService>> _loggerMock;
        protected Mock<HttpMessageHandler> _messageHandlerMock;
        protected Fixture _fixture;
        protected AutoMocker _mocker;
        protected WeatherClientService _service;

        public ClientServiceBaseTests()
        {
            this._fixture = new Fixture();
            this._mocker = new AutoMocker();

            this._clientSettings = this._fixture.Build<WeatherClientSettings>().Create();
            this._optionsClientSettingsMock = this._mocker.GetMock<IOptions<WeatherClientSettings>>();
            this._optionsClientSettingsMock.Setup(s => s.Value).Returns(this._clientSettings);
            this._loggerMock = this._mocker.GetMock<ILogger<WeatherClientService>>();
            this._messageHandlerMock = this._mocker.GetMock<HttpMessageHandler>();
            this._mocker.Use(this._optionsClientSettingsMock);
            this._service = this._mocker.CreateInstance<WeatherClientService>();
        }

        public void VerifyLogError(Times times)
        {
            this._loggerMock.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Error),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => true),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), times);
        }

        protected void GetHttpMessageHandlerMock(HttpResponseMessage responseMessage)
        {
            this._messageHandlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(responseMessage);
        }
    }
}
