using FindMeFoodTruck.DAL.Cosmos;
using FindMeFoodTrucks.Ingestor.Helpers;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace FindMeFoodTrucks.Ingestor
{
    [ExcludeFromCodeCoverage]
    public static class DataIngestor
    {
        [FunctionName("DataIngestor")]
        public static void Run(
            [TimerTrigger("0 0 */2 * * *")] TimerInfo myTimer, ILogger log)
        {
            try
            {
                log.LogInformation($"Starting scheduled task {DateTime.Now}");

                //Set Configuration Object
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddEnvironmentVariables()
                    .AddJsonFile("./local.settings.json", true)
                    .Build();

                //Get Web response
                WebRequestHelper wHelper = new WebRequestHelper(log);

                //DAL
                CosmosDAL cDAL = new CosmosDAL(config[ConstantStrings.COSMOS_ENDPOINT_URL],
                    config[ConstantStrings.COSMOS_PRIMARY_KEY],
                    config[ConstantStrings.COSMOS_DATABASE_NAME],
                    config[ConstantStrings.COSMOS_CONTAINER_NAME],
                    log);

                //Instantiate Orchestrator
                Orchestrator orc = new Orchestrator();

                //Perform synchronization
                orc.SynchronizeData(wHelper, cDAL, config, log);

                log.LogInformation($"Completed scheduled task {DateTime.Now}");
            }
            catch (Exception e)
            {
                log.LogError($"There was an error executing the task : {e.Message}");
                throw;
            }
        }
    }
}
