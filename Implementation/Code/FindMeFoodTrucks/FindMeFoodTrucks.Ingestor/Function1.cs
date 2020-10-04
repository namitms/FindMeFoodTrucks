using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace FindMeFoodTrucks.Ingestor
{
    public static class DataIngestor
    {
        [FunctionName("DataIngestor")]
        public static void Run([TimerTrigger("0 00 0 * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"This is a demo function: {DateTime.Now}");
        }
    }
}
