using FindMeFoodTruck.DAL.Cosmos;
using FindMeFoodTrucks.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FindMeFoodTrucks.Ingestor.Helpers
{
    //Data write Orchestrator
    public class Orchestrator
    {
        public void SynchronizeData(WebRequestHelper wHelper, CosmosDAL cDAL, IConfiguration config, ILogger log)
        {
            // Extract URL from config
            string url = config[ConstantStrings.DATA_API_URL];

            //Get Web response
            var jsonResponse = wHelper.GetResponse(url).Result;

            //Load list
            var foodtrucks = JsonConvert.DeserializeObject<List<FoodFacility>>(jsonResponse);

            //Write to Cosmos
            cDAL.WriteData(foodtrucks);
        }
    }
}
