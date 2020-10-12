using FindMeFoodTrucks.Models;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FindMeFoodTrucks.WebAPI.Helpers
{
    /// <summary>
    /// Query helper for Cosmos DAL
    /// </summary>
    public static class QueryHelper
    {
        /// <summary>
        /// Cosmos query 
        /// </summary>
        private static readonly string QUERY = "SELECT f.id , f.fooditems, f.applicant, f.address,  ST_DISTANCE(f.location, {0}) distance FROM c f WHERE ST_DISTANCE(f.location, {1}) < @radius AND f.facilitytype = 'Truck' {2}";
        /// <summary>
        /// Subquery for search strings
        /// </summary>
        private static readonly string SEARCH_SUBQUERY = "AND CONTAINS(UPPER(f.fooditems), UPPER(@searchstring))";
        /// <summary>
        /// Create cosmos query definition
        /// </summary>
        /// <param name="radius">radius</param>
        /// <param name="latitude">latitude</param>
        /// <param name="longitude">longitude</param>
        /// <param name="searchString">searchString</param>
        /// <returns>QueryDefinition</returns>
        public static QueryDefinition CreateCosmosQuery(long radius, double latitude, double longitude, string searchString)
        {
            if (longitude <= 180 &&
                longitude >= -180 &&
                latitude <= 90 &&
                latitude >= -90 && 
                radius >=0 )
            {
                string searchSubquery = string.Empty;
                if (searchString != null && !string.IsNullOrEmpty(searchString.Trim()))
                {
                    searchSubquery = string.Format(SEARCH_SUBQUERY, searchString.Trim());
                }

                Location loc = new Location();
                loc.coordinates = new List<double>();
                loc.coordinates.Add(longitude);
                loc.coordinates.Add(latitude);
                var locString = JsonConvert.SerializeObject(loc);
                var query = string.Format(QUERY, locString, locString, searchSubquery);
                QueryDefinition qd = new QueryDefinition(query)
                                         .WithParameter("@searchstring", searchString)
                                         .WithParameter("@radius", radius);
                return qd;
            }
            else
            {
                throw new ArgumentOutOfRangeException("Input parameters are out of range");
            }
        }
    }
}
