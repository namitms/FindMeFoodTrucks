using FindMeFoodTrucks.Models;
using Microsoft.Azure.Cosmos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FindMeFoodTruck.DAL.Cosmos
{
    /// <summary>
    /// Cosmos DAL interface
    /// </summary>
    public interface ICosmosDAL
    {
        /// <summary>
        /// Write records into the cosmos datastore
        /// </summary>
        /// <param name="foodFacilities">list of food facilities</param>
        /// <returns></returns>
        Task WriteData(List<FoodFacility> foodFacilities);
        /// <summary>
        /// Queries the datastore 
        /// </summary>
        /// <param name="queryDefinition">Datastore query</param>
        /// <returns></returns>
        Task<List<FoodFacilityResponse>> QueryData(QueryDefinition queryDefinition);
    }
}
