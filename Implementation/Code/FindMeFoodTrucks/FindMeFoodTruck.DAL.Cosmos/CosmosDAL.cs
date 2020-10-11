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
    public class CosmosDAL : ICosmosDAL
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
        /// The only constructor used by business logic
        /// </summary>
        /// <param name="endpointUri">endpointUri</param>
        /// <param name="primaryKey">primaryKey</param>
        /// <param name="dabaseName">dabaseName</param>
        /// <param name="containerName">containerName</param>
        /// <param name="logger">logger</param>
        public CosmosDAL(string endpointUri, string primaryKey, string dabaseName, string containerName)
        {
            Initialize(endpointUri, primaryKey, dabaseName, containerName);
        }

        /// <summary>
        /// MOQ constructor used for testing
        /// </summary>
        /// <param name="cClient">cClient</param>
        /// <param name="cDB">cDB</param>
        /// <param name="cCon">cCon</param>
        /// <param name="log">log</param>
        public CosmosDAL(CosmosClient cClient, Database cDB, Container cCon)
        {
            // Create a new instance of the Cosmos Client
            this.cosmosClient = cClient;

            // Create a new database by the name or get an existing one
            this.database = cDB;

            // Create a new container by the name or get an existing one
            this.container = cCon;
        }

        /// <summary>
        /// Initialize the database objects
        /// </summary>
        /// <param name="endpointUri">endpointUri</param>
        /// <param name="primaryKey">primaryKey</param>
        /// <param name="dabaseName">dabaseName</param>
        /// <param name="containerName">containerName</param>
        /// <param name="logger">logger</param>
        [ExcludeFromCodeCoverage]
        private void Initialize(string endpointUri, string primaryKey, string dabaseName, string containerName)
        {
            // Create a new instance of the Cosmos Client
            this.cosmosClient = new CosmosClient(endpointUri, primaryKey);

            // Create a new database by the name or get an existing one
            this.database = this.cosmosClient.CreateDatabaseIfNotExistsAsync(dabaseName).Result;

            // Create a new container by the name or get an existing one
            this.container = this.database.CreateContainerIfNotExistsAsync(containerName, "/id").Result;

        }

        /// <summary>
        /// Write records into the cosmos datastore
        /// </summary>
        /// <param name="foodFacilities">list of food facilities</param>
        /// <returns></returns>
        public virtual async Task WriteData(List<FoodFacility> foodFacilities)
        {
            //For every food facility in the list
            foreach (var curFF in foodFacilities)
            {
                try
                {
                    // Build Location object and populat id field
                    curFF.TransformData();
                    // Read the item to see if it exists.  
                    ItemResponse<FoodFacility> ffItem = await container.ReadItemAsync<FoodFacility>(curFF.id, new PartitionKey(curFF.id));

                    // Try update as the item already exist
                    await container.UpsertItemAsync<FoodFacility>(ffItem, new PartitionKey(curFF.id));
                }
                catch
                {
                    // If the item does not exist, create a new one
                    ItemResponse<FoodFacility> andersenFamilyResponse = await container.CreateItemAsync<FoodFacility>(curFF, new PartitionKey(curFF.id));
                }
            }
        }

        /// <summary>
        /// Queries the datastore 
        /// </summary>
        /// <param name="queryDefinition">Datastore query</param>
        /// <returns></returns>
        public virtual async Task<List<FoodFacilityResponse>> QueryData(QueryDefinition queryDefinition)
        {
            // Retrieve records based on the query
            FeedIterator<FoodFacilityResponse> queryResultSetIterator = this.container.GetItemQueryIterator<FoodFacilityResponse>(queryDefinition);
            var boo = queryDefinition.ToString();
            List<FoodFacilityResponse> foodTrucks = new List<FoodFacilityResponse>();

            while (queryResultSetIterator != null && queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<FoodFacilityResponse> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foodTrucks = currentResultSet.ToList<FoodFacilityResponse>();
            }

            //Reorder the records by distance
            foodTrucks = foodTrucks.OrderBy(f => f.distance).ToList();

            return foodTrucks;
        }
    }
}
