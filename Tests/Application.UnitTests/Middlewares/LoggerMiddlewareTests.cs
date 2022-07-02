using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Weather.API.Common.Exceptions;
using Xunit;

namespace Application.UnitTests.Middlewares
{
    public class LoggerMiddlewareTests
    {
        private readonly Mock<ILogger<LoggingMiddleware>> _loggerMock;

        public LoggerMiddlewareTests()
        {
            _loggerMock = new Mock<ILogger<LoggingMiddleware>>();
        }

        [Fact]
        public async Task Should_Always_Log_Response_And_Request()
        {
            DefaultHttpContext defaultContext = new DefaultHttpContext();

            var middlewareInstance = new LoggingMiddleware(context => Task.CompletedTask, _loggerMock.Object);
            await middlewareInstance.Invoke(defaultContext);

            _loggerMock.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == LogLevel.Information),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => true),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), Times.Exactly(2));
        }
    }
}
