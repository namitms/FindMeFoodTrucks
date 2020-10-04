using FindMeFoodTruck.DAL.Cosmos;
using FindMeFoodTrucks.Models;
using FindMeFoodTrucks.WebAPI.Filters;
using FindMeFoodTrucks.WebAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace FindMeFoodTrucks.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    ///Used for Authentication 
    [ServiceFilter(typeof(APIKeyAuthAttribute))]
    public class FoodTruckController : ControllerBase
    {
        private readonly ILogger<FoodTruckController> logger;
        private readonly IConfiguration configuration;
        private readonly CosmosDAL cosmosDAL;
        public FoodTruckController(ILogger<FoodTruckController> logger, IConfiguration configuration, CosmosDAL cDAL = null)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.cosmosDAL = cDAL;
            if (cosmosDAL == null)
            {
                cosmosDAL = new CosmosDAL(configuration[ConstantStrings.COSMOS_ENDPOINT_URL],
                    configuration[ConstantStrings.COSMOS_PRIMARY_KEY],
                    configuration[ConstantStrings.COSMOS_DATABASE_NAME],
                    configuration[ConstantStrings.COSMOS_CONTAINER_NAME],
                    logger);
            }
        }

        [HttpGet]
        public List<FoodFacilityResponse> Get(long radius, double longitude, double latitide, string searchString)
        {
            logger.LogInformation("FoodTruck requested through Web API");
            try
            {
                string query = QueryHelper.CreateCosmosQuery(radius, latitide, longitude, searchString);

                logger.LogInformation("FoodTruck request complete");
                return cosmosDAL.QueryData(query).Result;
            }
            catch
            {
                logger.LogError("Encountered error during execution");
                throw;
            }
        }
    }
}
