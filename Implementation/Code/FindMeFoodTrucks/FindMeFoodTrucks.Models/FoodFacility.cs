using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace FindMeFoodTrucks.Models
{
    /// <summary>
    /// Model for a record from mobile food facility data store
    /// </summary>
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class FoodFacility
    {
        public string id { get; set; }
        public string objectid { get; set; }
        public string applicant { get; set; }
        public string facilitytype { get; set; }
        public string cnn { get; set; }
        public string locationdescription { get; set; }
        public string address { get; set; }
        public string blocklot { get; set; }
        public string block { get; set; }
        public string lot { get; set; }
        public string permit { get; set; }
        public string status { get; set; }
        public string fooditems { get; set; }
        public string x { get; set; }
        public string y { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string schedule { get; set; }
        public string received { get; set; }
        public string priorpermit { get; set; }
        public DateTime expirationdate { get; set; }
        public Location location { get; set; }
        public double distance { get; set; }

        /// <summary>
        /// Populate the object for Cosmos GeoSpacial operations
        /// </summary>
        public void TransformData()
        {
            this.id = this.objectid;
            if (this.longitude != null && this.latitude != null)
            {
                this.location = new Location();
                this.location.coordinates = new List<double>();
                this.location.coordinates.Add(double.Parse(this.longitude));
                this.location.coordinates.Add(double.Parse(this.latitude));
            }
        }
    }

    /// <summary>
    /// Model for a record from mobile food facility data store
    /// </summary>
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class FoodFacilityResponse
    {
        public string id { get; set; }
        public string applicant { get; set; }
        public string address { get; set; }
        public string fooditems { get; set; }
        public double distance { get; set; }
    }

    /// <summary>
    /// Map for Location data type in Cosmos
    /// </summary>
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class Location
    {
        /// <summary>
        /// Const string for cosmos point
        /// </summary>
        private const string POINT = "Point";
        public string type = POINT;
        public List<double> coordinates { get; set; }
    }
}
