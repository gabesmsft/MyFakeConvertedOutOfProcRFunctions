using System.IO;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker;

namespace FunctionApp1
{
    public class BlobTrigger1
    {
        [Function("BlobTrigger1")]
        public static void Run([BlobTrigger("test-samples-trigger/{name}", Connection = "blobconn")] string myTriggerItem,
        [BlobInput("test-samples-output/{name}-output.txt", FileAccess.Write, Connection = "blobconn")] TextWriter myBlobOut,
        [BlobInput("test-samples-input/sample1.txt", FileAccess.Read, Connection = "blobconn")] string myBlob,
        ILogger logger
        )
        {
            logger.LogInformation($"Triggered Item = {myTriggerItem}");
            logger.LogInformation($"Input Item = {myBlob}");

            myBlobOut.WriteLine(myBlob);
            myBlobOut.Close();
            myBlobOut = null;
        }
    }
}
