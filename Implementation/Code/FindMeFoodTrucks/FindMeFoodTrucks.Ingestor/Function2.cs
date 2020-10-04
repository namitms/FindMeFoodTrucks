using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;

namespace FindMeFoodTrucks.Ingestor
{
    public static class Function2
    {
        [FunctionName("Function2")]
        public static void Run([TimerTrigger("0 0/5 0 ? * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
