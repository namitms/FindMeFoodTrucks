using System;
using System.Diagnostics.CodeAnalysis;

namespace FindMeFoodTrucks.Models
{

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
}
