using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Weather.API;
using Xunit;

namespace Application.IntegrationTests.Setup
{
    public abstract class BaseSetup : IClassFixture<WebApplicationFactory<Startup>>
    {
        private const string CombinePath = "appsettings.json";
        protected readonly HttpClient Client;
        protected BaseSetup(WebApplicationFactory<Startup> factory)
        {
            this.Client = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, conf) =>
                {
                    var projectDir = Directory.GetCurrentDirectory();
                    var configPath = Path.Combine(projectDir, CombinePath);
                    conf.AddJsonFile(configPath);
                });
            }).CreateClient();
        }

        protected int GetRandomInt(int a = 1, int b = 10) => new Random().Next(a, b);

        protected async Task<HttpResponseMessage> ClientCall<TRequest>(TRequest obj,
            HttpMethod methodType,
            string requestUri)
            where TRequest : class
        {
            var request = new HttpRequestMessage(methodType, requestUri);
            if (obj != null)
            {
                var serializeObject = JsonConvert.SerializeObject(obj);
                request.Content = new StringContent(serializeObject);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            return await this.Client.SendAsync(request);
        }


        protected async Task<TResponse> ReadFromResponse<TResponse>(HttpResponseMessage response)
        {
            var contentString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(contentString);
        }
    }
}
