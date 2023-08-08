using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;

namespace FunctionApp1
{
    public class EventHubTrigger1
    {
        private readonly ILogger _logger;

        public EventHubTrigger1(ILogger<EventHubTrigger1> logger)
        {
            _logger = logger;
        }

        [Function("EventHubTrigger1")]
        [return: EventHub("eventhub2", Connection = "EventHubConn1")]
        public async Task<string> Run([EventHubTrigger("eventhub1", Connection = "EventHubConn1")] EventData[] events)
        {
            var exceptions = new List<Exception>();

            foreach (EventData eventData in events)
            {
                try
                {
                    _logger.LogInformation($"EventHubTrigger1 function processed a message: {eventData.EventBody}");
                    await Task.Yield();
                }
                catch (Exception e)
                {
                    // We need to keep processing the rest of the batch - capture this exception and continue.
                    // Also, consider capturing details of the message that failed processing so it can be processed again later.
                    exceptions.Add(e);
                }
            }

            // Once processing of the batch is complete, if any messages in the batch failed processing throw an exception so that there is a record of the failure.

            if (exceptions.Count > 1)
                throw new AggregateException(exceptions);

            if (exceptions.Count == 1)
                throw exceptions.Single();

            return $"{DateTime.Now}";
        }
    }
}
