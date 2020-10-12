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
        public static QueryDefinition CreateCosmosQuery(long radius, double latitude, double longitude, string searchString)
        {
            if (longitude <= 180 &&
                longitude >= -180 &&
                latitude <= 90 &&
                latitude >= -90)
            {
                string query = "SELECT f.id , f.fooditems, f.applicant, f.address,  ST_DISTANCE(f.location, @location) distance FROM c f WHERE ST_DISTANCE(f.location, {1}) < @radius AND f.facilitytype = 'Truck' {2}";
                string searchSubQuery = "AND CONTAINS(UPPER(f.fooditems), UPPER(@searchstring))";
                if (searchString != null && !string.IsNullOrEmpty(searchString.Trim()))
                {
                    searchSubQuery = string.Format(searchSubQuery, searchString.Trim());
                }
                else
                {
                    searchSubQuery = string.Empty;
                }

                Location loc = new Location();
                loc.coordinates = new List<double>();
                loc.coordinates.Add(longitude);
                loc.coordinates.Add(latitude);
                var locString = JsonConvert.SerializeObject(loc);
                query = string.Format(query, locString, locString, searchSubQuery);
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
