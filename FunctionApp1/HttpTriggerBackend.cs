using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.IO;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace FunctionsDotNetDapr
{
    public class HttpTriggerBackend
    {
        private readonly ILogger _logger;

        public HttpTriggerBackend(ILogger<HttpTriggerBackend> logger)
        {
            _logger = logger;
        }


        private static HttpClient httpClient = new HttpClient();

        [Function("HttpTriggerBackend")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "HttpTriggerBackend")] HttpRequestData req)
        {
            _logger.LogInformation("HttpTriggerWithDaprServiceInvocationbackendend Function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            return new OkObjectResult($"{requestBody}");
        }
    }
}
