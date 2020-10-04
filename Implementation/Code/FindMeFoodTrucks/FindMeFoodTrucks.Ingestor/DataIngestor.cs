using FindMeFoodTruck.DAL.Cosmos;
using FindMeFoodTrucks.Ingestor.Helpers;
using FindMeFoodTrucks.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace FindMeFoodTrucks.Ingestor
{
    public static class DataIngestor
    {
        [FunctionName("DataIngestor")]
        public static void Run(
            [TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log)
        {
            try
            {
                log.LogInformation($"Starting scheduled task {DateTime.Now}");
                //Set Configuration Object
                var builder = new ConfigurationBuilder() 
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddEnvironmentVariables()
                    .AddJsonFile("./local.settings.json")
                    .Build();

                //Extract URL from config
                string url = builder[ConstantStrings.DATA_API_URL];

                //Get Web response
                WebRequestHelper wHelper = new WebRequestHelper(log);
                var jsonResponse = wHelper.GetResponse(url).Result;

                var foodtrucks = JsonConvert.DeserializeObject<List<FoodFacility>>(jsonResponse);

                CosmosDAL cDAL = new CosmosDAL(builder[ConstantStrings.COSMOS_ENDPOINT_URL],
                    builder[ConstantStrings.COSMOS_PRIMARY_KEY],
                    builder[ConstantStrings.COSMOS_DATABASE_NAME],
                    builder[ConstantStrings.COSMOS_CONTAINER_NAME],
                    log);
                cDAL.WriteData(foodtrucks);

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
