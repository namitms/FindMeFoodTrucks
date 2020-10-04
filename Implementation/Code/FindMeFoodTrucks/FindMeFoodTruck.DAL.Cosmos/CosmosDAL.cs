using FindMeFoodTrucks.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace FindMeFoodTruck.DAL.Cosmos
{
    /// <summary>
    /// DAL for Cosmos DB
    /// </summary>
    public class CosmosDAL
    {
        /// <summary>
        /// The Cosmos client instance
        /// </summary>
        private CosmosClient cosmosClient;
        /// <summary>
        /// The database we will create
        /// </summary>
        private Database database;
        /// <summary>
        /// The container we will create.
        /// </summary>
        private Container container;
        /// <summary>
        /// Logging Object
        /// </summary>
        private ILogger logger = null;


        public CosmosDAL(string endpointUri, string primaryKey, string dabaseName, string containerName, ILogger logger)
        {
            Initialize(endpointUri, primaryKey, dabaseName, containerName, logger);
        }

        public CosmosDAL(CosmosClient cClient, Database cDB, Container cCon, ILogger log)
        {
            // Create a new instance of the Cosmos Client
            this.cosmosClient = cClient;

            // Create a new database by the name or get an existing one
            this.database = cDB;

            // Create a new container by the name or get an existing one
            this.container = cCon;

            // Initialize logger
            this.logger = log;
        }

        [ExcludeFromCodeCoverage]
        private void Initialize(string endpointUri, string primaryKey, string dabaseName, string containerName, ILogger logger)
        {
            // Create a new instance of the Cosmos Client
            this.cosmosClient = new CosmosClient(endpointUri, primaryKey);

            // Create a new database by the name or get an existing one
            this.database = this.cosmosClient.CreateDatabaseIfNotExistsAsync(dabaseName).Result;

            // Create a new container by the name or get an existing one
            this.container = this.database.CreateContainerIfNotExistsAsync(containerName, "/id").Result;

            // Initialize logger
            this.logger = logger;
        }

        /// <summary>
        /// Write records into the cosmos datastore
        /// </summary>
        /// <param name="foodFacilities">list of food facilities</param>
        /// <returns></returns>
        public virtual void WriteData(List<FoodFacility> foodFacilities)
        {
            foreach (var curFF in foodFacilities)
            {
                try
                {
                    curFF.TransformData();
                    // Read the item to see if it exists.  
                    ItemResponse<FoodFacility> ffItem = container.ReadItemAsync<FoodFacility>(curFF.id, new PartitionKey(curFF.id)).Result;
                    // If it does, update the item
                    logger.LogInformation($"Record found.Trying update {curFF.id}");
                    container.UpsertItemAsync<FoodFacility>(ffItem, new PartitionKey(curFF.id));
                }
                catch
                {
                    logger.LogInformation($"Record not found.Trying insert {curFF.id}");
                    // If the item does not exist, create a new one
                    ItemResponse<FoodFacility> andersenFamilyResponse = container.CreateItemAsync<FoodFacility>(curFF, new PartitionKey(curFF.id)).Result;
                }
            }
        }

        /// <summary>
        /// Queries the datastore 
        /// </summary>
        /// <param name="queryString">Datastore query</param>
        /// <returns></returns>
        public virtual async Task<List<FoodFacilityResponse>> QueryData(string queryString)
        {
            QueryDefinition queryDefinition = new QueryDefinition(queryString);
            FeedIterator<FoodFacility> queryResultSetIterator = this.container.GetItemQueryIterator<FoodFacility>(queryDefinition);

            List<FoodFacilityResponse> foodTrucks = new List<FoodFacilityResponse>();

            while (queryResultSetIterator != null && queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<FoodFacility> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (FoodFacility ft in currentResultSet)
                {
                    FoodFacilityResponse curRes = new FoodFacilityResponse();
                    curRes.id = ft.id;
                    curRes.address = ft.address;
                    curRes.applicant = ft.applicant;
                    curRes.fooditems = ft.fooditems;
                    curRes.distance = ft.distance;
                    foodTrucks.Add(curRes);
                }
            }

            //Reorder the records by distance
            foodTrucks = foodTrucks.OrderBy(f => f.distance).ToList();

            return foodTrucks;
        }
    }
}
