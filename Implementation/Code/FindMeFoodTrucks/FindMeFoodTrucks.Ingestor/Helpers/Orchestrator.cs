using FindMeFoodTruck.DAL.Cosmos;
using FindMeFoodTrucks.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindMeFoodTrucks.Ingestor.Helpers
{
    //Data write Orchestrator
    public class Orchestrator
    {
        /// <summary>
        /// Orchstrates the data ingestion
        /// </summary>
        /// <param name="wHelper">Web helper</param>
        /// <param name="cDAL">Cosmos DAL</param>
        /// <param name="config">Config</param>
        /// <param name="log">Logger</param>
        public async Task SynchronizeData(WebRequestHelper wHelper, CosmosDAL cDAL, IConfiguration config, ILogger log)
        {
            // Extract URL from config
            string url = config[ConstantStrings.DATA_API_URL];

            //Get Web response
            var jsonResponse = await wHelper.GetResponse(url);

            //Load list
            var foodtrucks = JsonConvert.DeserializeObject<List<FoodFacility>>(jsonResponse);

            //Write to Cosmos
            await cDAL.WriteData(foodtrucks);
        }
    }
}
