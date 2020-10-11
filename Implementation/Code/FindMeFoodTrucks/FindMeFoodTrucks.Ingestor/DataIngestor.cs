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
    /// <summary>
    /// Azure function scheduled to run every 12 hours for data ingestion from the SF Web API
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class DataIngestor
    {
        /// <summary>
        /// The only Run method of the function
        /// </summary>
        /// <param name="myTimer">Timer preset to 12 hours</param>
        /// <param name="log">Logger</param>
        [FunctionName("DataIngestor")]
        public static async void Run(
            [TimerTrigger("0 0 */12 * * *")] TimerInfo myTimer, ILogger log)
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
                    config[ConstantStrings.COSMOS_CONTAINER_NAME]);

                //Instantiate Orchestrator
                Orchestrator orc = new Orchestrator();

                //Perform synchronization
                await orc.SynchronizeData(wHelper, cDAL, config, log);

                log.LogInformation($"Completed scheduled task {DateTime.Now}");
            }
            catch (Exception e)
            {
                log.LogError(e, $"There was an error executing the task");
                throw;
            }
        }
    }
}
