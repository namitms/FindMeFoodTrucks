using FindMeFoodTruck.DAL.Cosmos;
using FindMeFoodTrucks.Models;
using FindMeFoodTrucks.WebAPI.Filters;
using FindMeFoodTrucks.WebAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace FindMeFoodTrucks.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    ///Used for Authentication 
    [ServiceFilter(typeof(APIKeyAuthAttribute))]
    public class FoodTruckController : ControllerBase
    {
        /// <summary>
        /// Logger instance
        /// </summary>
        private readonly ILogger<FoodTruckController> logger;
        /// <summary>
        /// Configuration instance
        /// </summary>
        private readonly IConfiguration configuration;
        /// <summary>
        /// Cosmos DAL instance
        /// </summary>
        private readonly CosmosDAL cosmosDAL;

        /// <summary>
        /// Only constructor
        /// </summary>
        /// <param name="logger">logger</param>
        /// <param name="configuration">configuration</param>
        /// <param name="cDAL">DAL for Cosmos</param>
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

        /// <summary>
        /// The get method use to search for food trucks
        /// </summary>
        /// <param name="radius">Radius in meters from the point where the search should be performed</param>
        /// <param name="longitude">The longitude of the center point</param>
        /// <param name="latitude">The latitude of the center point</param>
        /// <param name="searchString">Search string for food preferences</param>
        /// <returns></returns>
        [HttpGet]
        public List<FoodFacilityResponse> Get(long radius, double longitude, double latitude, string searchString)
        {
            logger.LogInformation("FoodTruck requested through Web API");
            try
            {
                //Construct query string based on teh input parameters
                var query = QueryHelper.CreateCosmosQuery(radius, latitude, longitude, searchString);

                logger.LogInformation("FoodTruck request complete");
                return cosmosDAL.QueryData(query).Result;
            }
            catch(Exception e)
            {
                logger.LogError(e,"Encountered error during execution");
                throw new System.Web.Http.HttpResponseException(System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}
