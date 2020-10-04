using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;

namespace FindMeFoodTrucks.Ingestor
{
    public static class DataIngestor
    {
        [FunctionName("DataIngestor")]
        public static void Run([TimerTrigger("0 00 0 * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"This is a demo function: {DateTime.Now}");
        }
    }
}
