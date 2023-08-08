using System;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;

namespace FunctionApp1
{
    public class ServiceBusQueueTrigger1
    {
        [Function("ServiceBusQueueTrigger1")]
        [return: ServiceBus("myqueue2ForProcTest", Connection = "ServiceBusConnection")]
        public static string Run(
    [ServiceBusTrigger("myqueueForProcTest", Connection = "ServiceBusConnection")]
    string myQueueItem,
    Int32 deliveryCount,
    DateTime enqueuedTimeUtc,
    string messageId,
    ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
            log.LogInformation($"EnqueuedTimeUtc={enqueuedTimeUtc}");
            log.LogInformation($"DeliveryCount={deliveryCount}");
            log.LogInformation($"MessageId={messageId}");
            return myQueueItem;
        }
    }
}
