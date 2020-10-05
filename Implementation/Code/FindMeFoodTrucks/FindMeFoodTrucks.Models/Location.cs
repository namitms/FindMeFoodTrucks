using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace FindMeFoodTrucks.Models
{
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
