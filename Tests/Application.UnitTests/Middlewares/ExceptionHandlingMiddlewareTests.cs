using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Weather.API.Common.Exceptions;
using Weather.API.Common.Middlewares;
using Xunit;

namespace Application.UnitTests.Middlewares
{
    public class ExceptionHandlingMiddlewareTests
    {
        private readonly Mock<ILogger<ExceptionHandlingMiddleware>> _loggerMock;

        public ExceptionHandlingMiddlewareTests()
        {
            _loggerMock = new Mock<ILogger<ExceptionHandlingMiddleware>>();
        }

        [Fact]
        public async Task Should_Throw_My_Custom_Exception_With_Message()
        {
            var middleware = new ExceptionHandlingMiddleware
            (context => throw new WeatherMyAppException(
                It.IsAny<string>(),
                HttpStatusCode.BadRequest), _loggerMock.Object);

            var httpContext = new DefaultHttpContext();

            await middleware.Invoke(httpContext);

            Assert.Equal(StatusCodes.Status400BadRequest, httpContext.Response.StatusCode);
        }

        [Fact]
        public async Task Should_Throw_My_Custom_Exception_With_Errors()
        {
            var middleware = new ExceptionHandlingMiddleware
            (context => throw new WeatherMyAppException(
                It.IsAny<string>(),
                HttpStatusCode.BadRequest, It.IsAny<List<string>>()), _loggerMock.Object);

            var httpContext = new DefaultHttpContext();

            await middleware.Invoke(httpContext);

            Assert.Equal(StatusCodes.Status400BadRequest, httpContext.Response.StatusCode);
        }

        [Fact]
        public async Task Should_Be_InternalServerError_When_Another_Exception_Than_Custom_Exception()
        {
            var middleware = new ExceptionHandlingMiddleware
            (context => throw new Exception(
                It.IsAny<string>()), _loggerMock.Object);

            var httpContext = new DefaultHttpContext();

            await middleware.Invoke(httpContext);

            Assert.Equal(StatusCodes.Status500InternalServerError, httpContext.Response.StatusCode);
        }
    }
}
