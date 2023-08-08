using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Collections.Generic;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace FunctionApp1
{
    public class HttpTriggerFrontend
    {
        private readonly ILogger _logger;

        public HttpTriggerFrontend(ILogger<HttpTriggerFrontend> logger)
        {
            _logger = logger;
        }

        private static string HttpProtocol = !System.Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME").Contains("localhost") ? "https" : "http";


        private static HttpClient httpClient = new HttpClient();

        [Function("HttpTriggerFrontend")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "HttpTriggerFrontend")] HttpRequestData req)
        {
            _logger.LogInformation("HttpTriggerFrontend Function processed a request.");

            string HttpTriggerBackenURL = @$"{HttpProtocol}://" + System.Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME") + "/api/HttpTriggerBackend";

            var values = new Dictionary<string, string>();
            values.Add("Donuts", "Maple");
            var content = new FormUrlEncodedContent(values);

            HttpRequestData request = new HttpRequestData(HttpMethod.Post, HttpTriggerBackenURL)
            {
                Content = content
            };

            HttpResponseData response = await httpClient.SendAsync(request);
            string responseBody = await response.Content.ReadAsStringAsync();

            return new OkObjectResult(responseBody);
        }
    }
}
