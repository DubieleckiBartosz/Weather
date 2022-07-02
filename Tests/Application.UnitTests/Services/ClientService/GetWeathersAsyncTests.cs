using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using Xunit;

namespace Application.UnitTests.Services.ClientService
{
    public class GetWeathersAsyncTests : ClientServiceBaseTests
    {
        [Fact]
        public async Task Should_Return_Null_When_StatusCode_Not_Success()
        {
            var responseError = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                ReasonPhrase = It.IsAny<string>(),
                Content = new StringContent("{\"error\":{\"message\":\"error\"}}")
            };
            GetHttpMessageHandlerMock(responseError);

            var result = await _service.GetWeathersAsync(It.IsAny<string>(), It.IsAny<string>());
            VerifyLogError(Times.Once());
            Assert.Null(result);
        }

        [Fact]
        public async Task Should_Return_List_of_Weathers_When_Status_Code_Success()
        {
            var responseError = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                ReasonPhrase = It.IsAny<string>(),
                Content = new StringContent("{\"location\": {\"name\": \"city\"}}")
            };
            GetHttpMessageHandlerMock(responseError);

            var result = await _service.GetWeathersAsync(It.IsAny<string>(), It.IsAny<string>());
            VerifyLogError(Times.Never());
            Assert.NotNull(result);
        }
    }
}
